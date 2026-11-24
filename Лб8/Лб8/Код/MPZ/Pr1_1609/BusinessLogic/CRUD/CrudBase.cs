using Pr1_1609.Domain;
using Pr1_1609.DataAccess;
using Pr1_1609.BusinessLogic.Navigation;

namespace Pr1_1609.BusinessLogic.CRUD
{
    public abstract class CrudBase<T> where T : IEntity
    {
        protected readonly DB db;
        protected readonly StateMachine stateMachine;

        protected CrudBase(DB db, StateMachine stateMachine)
        {
            this.db = db;
            this.stateMachine = stateMachine;
        }

        public abstract int Create();
        public abstract void Read();
        public abstract void Update();
        public abstract void Delete();
        public abstract void GetAll();
    }
}
