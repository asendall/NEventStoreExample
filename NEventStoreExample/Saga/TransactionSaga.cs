using CommonDomain.Core;
using NEventStoreExample.Command;
using NEventStoreExample.Event;
using NEventStoreExample.Infrastructure;
using Stateless;

namespace NEventStoreExample.Saga
{
    public class TransactionSaga:SagaBase<IMessage>
    {
        private enum State
        {
            SagaInitialised,
            DebitingAccount1,
            CreditingAccount2,
            MarkingTransactionComplete,
            Complete
        }

        private enum Trigger
        {
            TransferReqested,
            Account1Debited,
            Account2Credited,
            TransactionComplete

        }

        private StateMachine<State, Trigger> _stateMachine;
        private TransactionCreatedEvent _ev;

        public TransactionSaga(string id)
        {
            Id = id;
            //Register events
            this.Register<TransactionCreatedEvent>(Transition);
            this.Register<AccountCreditedEvent>(Transition);
            this.Register<AccountDebitedEvent>(Transition);
            //Initialise state machine
            _stateMachine = new StateMachine<State, Trigger>(State.SagaInitialised);

            //Configure state machine
            _stateMachine.Configure(State.SagaInitialised).Permit(Trigger.TransferReqested, State.DebitingAccount1);
            _stateMachine.Configure(State.DebitingAccount1).Permit(Trigger.Account1Debited, State.CreditingAccount2);
            _stateMachine.Configure(State.CreditingAccount2).Permit(Trigger.Account2Credited, State.MarkingTransactionComplete);
            _stateMachine.Configure(State.MarkingTransactionComplete).Permit(Trigger.TransactionComplete, State.Complete);


            _stateMachine.Configure(State.DebitingAccount1)
                .OnEntry(OnDebitingAccount1);
            _stateMachine.Configure(State.CreditingAccount2)
                .OnEntry(OnCreditingAccount2);
            _stateMachine.Configure(State.MarkingTransactionComplete)
                .OnEntry(OnMarkingTransactionComplete);
        }

        private void Transition(TransactionCreatedEvent e)
        {
            _ev = e;
            _stateMachine.Fire(Trigger.TransferReqested);
        }

        private void Transition(AccountDebitedEvent e)
        {
            _stateMachine.Fire(Trigger.Account1Debited);
        }

        private void Transition(AccountCreditedEvent e)
        {
            _stateMachine.Fire(Trigger.Account2Credited);
        }

        private void OnDebitingAccount1()
        {
            this.Dispatch(new DebitAccountCommand(_ev.CorrelationId, _ev.AccountToCreditId, _ev.Amount));
        }

        private void OnCreditingAccount2()
        {
            this.Dispatch(new CreditAccountCommand(_ev.CorrelationId, _ev.AccountToCreditId, _ev.Amount));
        }

        private void OnMarkingTransactionComplete()
        {
            _stateMachine.Fire(Trigger.TransactionComplete);
        }

    }
}
