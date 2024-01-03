using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed internal class DalList : IDal
    {
        public static DalList Instance { get; } = new Lazy<DalList>(() => new DalList(), true).Value;
        private DalList() { }
        public IDependency Dependency =>  new DependencyImplementation();

        public IEngineer Engineer =>  new EngineerImplementation();

        public ITask Task =>  new TaskImplementation();
    }
}
