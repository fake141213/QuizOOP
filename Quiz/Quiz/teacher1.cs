using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{

    internal class teacher1
    {

        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string Major { get; private set; }

        private List<Students> _advisees;

        public teacher1(string name, string lastName, string major)
        {
            Name = name;
            LastName = lastName;
            Major = major;
            _advisees = new List<Students>();
        }

        public void AddAdvisee(Students student)
        {
            if (!_advisees.Contains(student))
            {
                _advisees.Add(student);
            }
        }

        public List<Students> GetAllAdvisees()
        {
            return _advisees.ToList();
        }
        public Students GetTopGradeStudent()
        {
            if (_advisees.Count == 0)
                return null;

            return _advisees.OrderByDescending(s => s.Grade).FirstOrDefault();
        }

        public int GetAdviseeCount()
        {
            return _advisees.Count;
        }

        public override string ToString()
        {
            return $"{Name} {LastName}, Major: {Major}, Advisees: {GetAdviseeCount()}";
        }
    }
}