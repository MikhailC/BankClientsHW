using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfApp1.Model
{
    public class EmployeeList:ObservableCollection<Employee>
    {
        private readonly List<Employee> _employees = new List<Employee>();
        public IEnumerator<Employee> GetEnumerator() => _employees.GetEnumerator();
      
        public IOperator Operator { get; set; }

        IEnumerator IEnumerable.GetEnumerator()=>_employees.GetEnumerator();
        

        public void Add(Employee item)
        {
            if (item != null)
            {
                item.list = this;
                _employees.Add(item);
            }
        }

        public void Clear() => _employees.Clear();


        public bool Contains(Employee item) => _employees.Contains(item);


        public void CopyTo(Employee[] array, int arrayIndex) => _employees.CopyTo(array, arrayIndex);


        public bool Remove(Employee item) => _employees.Remove(item);


        public int Count => _employees.Count;
        public bool IsReadOnly => false;
        public int IndexOf(Employee item) => _employees.IndexOf(item);

        public void Insert(int index, Employee item) => _employees.Insert(index, item);
       
        public void RemoveAt(int index) => _employees.RemoveAt(index);
      
        public Employee this[int index]
        {
            get => _employees[index];
            set => _employees[index]=value;
        }
    }

  
}