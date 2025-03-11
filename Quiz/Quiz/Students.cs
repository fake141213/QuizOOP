using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{

    internal class Students
    {

        public string Name { get; private set; }
        public string LastName { get; private set; }
        public string StudentID { get; private set; }
        public string Major { get; private set; }
        public double Grade { get; private set; }

        public teacher1 Advisor { get; private set; }


        public Students(string name, string lastName, string studentID, string major, double grade)
        {
            Name = name;
            LastName = lastName;
            StudentID = studentID;
            Major = major;
            Grade = grade;
        }

        public void SetAdvisor(teacher1 advisor)
        {
            Advisor = advisor;

            advisor.AddAdvisee(this);
        }

        public void UpdateGrade(double newGrade)
        {
            Grade = newGrade;
        }

        public override string ToString()
        {
            return $"{Name} {LastName} (ID: {StudentID}), Major: {Major}, Grade: {Grade}";
        }

        public string GetAdvisorInfo()
        {
            if (Advisor != null)
                return $"Advisor: {Advisor.Name} {Advisor.LastName}";
            else
                return "No advisor assigned";
        }
    }
}
