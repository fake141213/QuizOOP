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
            button2.Text = "�ѹ�֡�������Ҩ����";
            button3.Text = "�ʴ��ô�ѡ�֡�ҷ���٧����ش";
            button5.Text = "�ѹ�֡�����Źѡ�֡��";


            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;
            button5.Click += Button5_Click;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;


            this.Text = "�к��ѹ�֡�����Źѡ�֡������Ҩ�������֡��";


            InitializeLabels();
        }

        private void InitializeLabels()
        {

            label9.Text = "Name:";
            label10.Text = "Last Name:";
            label11.Text = "StudentID:";
            label13.Text = "Major:";
            label12.Text = "Grade:";
            label6.Text = "�ӹǹ�ѡ�֡��: 0";
            label15.Text = "�Ң�: -";
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
                MessageBox.Show("��سҡ�͡���������ú", "���������ú", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }


            var newTeacher = new teacher1(textBox7.Text, textBox6.Text, textBox8.Text);
            teachersList.Add(newTeacher);

            LoadTeachersToComboBox();

            MessageBox.Show("�ѹ�֡�������Ҩ�������º��������", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ClearTeacherInputs();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (teachersList.Count == 0)
            {
                MessageBox.Show("����բ������Ҩ����", "��������辺", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (comboBox1.SelectedIndex < 0)
            {
                MessageBox.Show("��س����͡�Ҩ����", "���������ú", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedTeacher = teachersList[comboBox1.SelectedIndex];

            var topStudent = selectedTeacher.GetTopGradeStudent();

            if (topStudent == null)
            {
                MessageBox.Show("����չѡ�֡��㹷���֡��", "��������辺", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            UpdateStudentInfoDisplay(topStudent);

            MessageBox.Show($"�ѡ�֡�ҷ�����ô�٧����ش: {topStudent.Name} {topStudent.LastName} �ô: {topStudent.Grade}",
                "�ѡ�֡���ô�٧�ش", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox9.Text) || string.IsNullOrEmpty(textBox5.Text) ||
                string.IsNullOrEmpty(textBox11.Text) || string.IsNullOrEmpty(textBox10.Text) || string.IsNullOrEmpty(textBox12.Text))
            {
                MessageBox.Show("��سҡ�͡���������ú", "���������ú", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!double.TryParse(textBox12.Text, out double grade))
            {
                MessageBox.Show("��سҡ�͡�ô�繵���Ţ", "���������١��ͧ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double[] allowedGrades = { 1.0, 1.5, 2.0, 2.5, 3.0, 3.5, 4.0 };
            if (!Array.Exists(allowedGrades, g => g == grade))
            {
                MessageBox.Show("�ô��ͧ�� 1.0, 1.5, 2.0, 2.5, 3.0, 3.5 ���� 4.0 ��ҹ��",
                                 "���������١��ͧ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string studentID = textBox11.Text;
            foreach (var student in studentsList)
            {
                if (student.StudentID == studentID)
                {
                    MessageBox.Show("���ʹѡ�֡�ҹ����������к�����", "�����ū��", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            MessageBox.Show("�ѹ�֡�����Źѡ�֡�����º��������", "�����", MessageBoxButtons.OK, MessageBoxIcon.Information);

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
                label6.Text = "�ӹǹ�ѡ�֡��: 0";
                label15.Text = "�Ң�: -";
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

                label6.Text = $"�ӹǹ�ѡ�֡��: {advisees.Count}";
                if (advisees.Count > 0)
                {
                    HashSet<string> majors = new HashSet<string>();
                    foreach (var advisee in advisees)
                    {
                        majors.Add(advisee.Major);
                    }
                    label15.Text = $"�Ң�: {string.Join(", ", majors)}";
                }
                else
                {
                    label15.Text = "�Ң�: -";
                }
            }
            else
            {
                label6.Text = "�ӹǹ�ѡ�֡��: 0";
                label15.Text = "�Ң�: -";
            }
        }
    }
}




