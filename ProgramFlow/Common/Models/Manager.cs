using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpProgramming.Common.Models
{
    /// <summary>
    /// Manager class implements an indexer for Employees
    /// </summary>
    public class Manager
    {
        private IList<Employee> _employees = new List<Employee>();

        public Manager() { }

        public Manager(IList<Employee> employees)
        {
            _employees = employees;
        }

        /// <summary>
        /// Indexer to return the employee with the specified Id.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public Employee this[int employeeId]
        {
            get
            {
                if (_employees != null && _employees.Count > 0)
                    return _employees.FirstOrDefault(e => e.EmployeeId == employeeId);
                return null;
            }
        }
    }
}
