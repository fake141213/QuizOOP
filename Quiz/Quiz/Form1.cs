using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Quiz
{
    public partial class Form1 : Form
    {
        private List<Students> studentsList;
        private List<teacher1> teachersList;

        public Form1()
        {
            InitializeComponent();

            studentsList = new List<Students>();
            teachersList = new List<teacher1>();

            button1.Text = "Reset";
            button2.Text = "บันทึกข้อมูลอาจารย์";
            button3.Text = "แสดงเกรดนักศึกษาที่สูงที่สุด";
            button5.Text = "บันทึกข้อมูลนักศึกษา";


            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            button5.Click += Button5_Click;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;


            this.Text = "ระบบบันทึกข้อมูลนักศึกษาและอาจารย์ที่ปรึกษา";


            InitializeLabels();
        }

        private void InitializeLabels()
        {

            label9.Text = "Name:";
            label10.Text = "Last Name:";
            label11.Text = "StudentID:";
            label13.Text = "Major:";
            label12.Text = "Grade:";
            label6.Text = "จำนวนนักศึกษา: 0";
            label15.Text = "สาขา: -";
        }

        private void LoadTeachersToComboBox()
        {

            comboBox1.Items.Clear();


            foreach (var teacher in teachersList)
            {
                comboBox1.Items.Add($"{teacher.Name} {teacher.LastName}");
            }
        }


        private void Button1_Click(object sender, EventArgs e)
        {
            ClearStudentInputs();
            ClearTeacherInputs();
            UpdateStudentInfoDisplay(null);
            UpdateAdviseeInfoDisplay();
        }


        private void Button2_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(textBox7.Text) || string.IsNullOrEmpty(textBox6.Text) || string.IsNullOrEmpty(textBox8.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ", "ข้อมูลไม่ครบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var newTeacher = new teacher1(textBox7.Text, textBox6.Text, textBox8.Text);
            teachersList.Add(newTeacher);

            LoadTeachersToComboBox();

            MessageBox.Show("บันทึกข้อมูลอาจารย์เรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearTeacherInputs();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (teachersList.Count == 0)
            {
                MessageBox.Show("ไม่มีข้อมูลอาจารย์", "ข้อมูลไม่พบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("กรุณาเลือกอาจารย์", "ข้อมูลไม่ครบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedTeacher = teachersList[comboBox1.SelectedIndex];

            var topStudent = selectedTeacher.GetTopGradeStudent();

            if (topStudent == null)
            {
                MessageBox.Show("ไม่มีนักศึกษาในที่ปรึกษา", "ข้อมูลไม่พบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            UpdateStudentInfoDisplay(topStudent);

            MessageBox.Show($"นักศึกษาที่มีเกรดสูงที่สุด: {topStudent.Name} {topStudent.LastName} เกรด: {topStudent.Grade}",
                "นักศึกษาเกรดสูงสุด", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox9.Text) || string.IsNullOrEmpty(textBox5.Text) ||
                string.IsNullOrEmpty(textBox11.Text) || string.IsNullOrEmpty(textBox10.Text) || string.IsNullOrEmpty(textBox12.Text))
            {
                MessageBox.Show("กรุณากรอกข้อมูลให้ครบ", "ข้อมูลไม่ครบ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(textBox12.Text, out double grade))
            {
                MessageBox.Show("กรุณากรอกเกรดเป็นตัวเลข", "ข้อมูลไม่ถูกต้อง", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double[] allowedGrades = { 1.0, 1.5, 2.0, 2.5, 3.0, 3.5, 4.0 };
            if (!Array.Exists(allowedGrades, g => g == grade))
            {
                MessageBox.Show("เกรดต้องเป็น 1.0, 1.5, 2.0, 2.5, 3.0, 3.5 หรือ 4.0 เท่านั้น",
                                 "ข้อมูลไม่ถูกต้อง", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string studentID = textBox11.Text;
            foreach (var student in studentsList)
            {
                if (student.StudentID == studentID)
                {
                    MessageBox.Show("รหัสนักศึกษานี้มีอยู่ในระบบแล้ว", "ข้อมูลซ้ำ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            var newStudent = new Students(textBox9.Text, textBox5.Text, studentID, textBox10.Text, grade);

            if (comboBox1.SelectedIndex >= 0)
            {
                newStudent.SetAdvisor(teachersList[comboBox1.SelectedIndex]);
            }
            studentsList.Add(newStudent);
            UpdateStudentInfoDisplay(newStudent);

            if (comboBox1.SelectedIndex >= 0)
            {
                UpdateAdviseeInfoDisplay();
            }

            MessageBox.Show("บันทึกข้อมูลนักศึกษาเรียบร้อยแล้ว", "สำเร็จ", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearStudentInputs();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0 && comboBox1.SelectedIndex < teachersList.Count)
            {
                UpdateAdviseeInfoDisplay();
            }
            else
            {
                label6.Text = "จำนวนนักศึกษา: 0";
                label15.Text = "สาขา: -";
            }
        }

        private void ClearStudentInputs()
        {
            textBox9.Text = string.Empty;
            textBox5.Text = string.Empty;
            textBox11.Text = string.Empty;
            textBox10.Text = string.Empty;
            textBox12.Text = string.Empty;
        }

        private void ClearTeacherInputs()
        {
            textBox7.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox8.Text = string.Empty;
        }

        private void UpdateStudentInfoDisplay(Students student)
        {
            if (student != null)
            {
                label9.Text = $"Name: {student.Name}";
                label10.Text = $"Last Name: {student.LastName}";
                label11.Text = $"StudentID: {student.StudentID}";
                label13.Text = $"Major: {student.Major}";
                label12.Text = $"Grade: {student.Grade}";
            }
            else
            {
                label9.Text = "Name:";
                label10.Text = "Last Name:";
                label11.Text = "StudentID:";
                label13.Text = "Major:";
                label12.Text = "Grade:";
            }
        }
        private void UpdateAdviseeInfoDisplay()
        {
            if (comboBox1.SelectedIndex >= 0 && comboBox1.SelectedIndex < teachersList.Count)
            {
                var selectedTeacher = teachersList[comboBox1.SelectedIndex];
                var advisees = selectedTeacher.GetAllAdvisees();

                label6.Text = $"จำนวนนักศึกษา: {advisees.Count}";
                if (advisees.Count > 0)
                {
                    HashSet<string> majors = new HashSet<string>();
                    foreach (var advisee in advisees)
                    {
                        majors.Add(advisee.Major);
                    }
                    label15.Text = $"สาขา: {string.Join(", ", majors)}";
                }
                else
                {
                    label15.Text = "สาขา: -";
                }
            }
            else
            {
                label6.Text = "จำนวนนักศึกษา: 0";
                label15.Text = "สาขา: -";
            }
        }
    }
}




