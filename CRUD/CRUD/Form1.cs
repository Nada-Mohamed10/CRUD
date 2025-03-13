using CRUD.Models;

namespace CRUD
{
    public partial class Form1 : Form
    {
        ITIContext db;
        int ID;
        public Form1()
        {
            InitializeComponent();
            db = new ITIContext();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = db.students.Select(n => new { n.id, n.First_Name, n.Last_Name, n.age, DeptName = n.Dept.Dept_Name }).ToList();

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            comp_dept.DataSource = db.Departments.ToList();
            comp_dept.ValueMember = "Dept_id";
            comp_dept.DisplayMember = "Dept_Name";
            comp_dept.SelectedIndex = -1;
            comp_dept.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            student s = new student()
            {
                First_Name = txt_Fname.Text,
                Last_Name = txt_Lname.Text,
                age = int.Parse(txt_Age.Text),
                Dept_id = (int)comp_dept.SelectedValue
            };

            db.students.Add(s);
            db.SaveChanges();
            MessageBox.Show("Added successfully!");
            emptyTextBox();
            button1_Click(null, null);
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
            student s = db.students.Where(n => n.id == ID).SingleOrDefault();
            txt_Fname.Text = s.First_Name;
            txt_Lname.Text = s.Last_Name;
            txt_Age.Text = s.age.ToString();
            comp_dept.SelectedValue = s.Dept_id;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            student s = db.students.Where(n => n.id == ID).SingleOrDefault();
            s.First_Name = txt_Fname.Text;
            s.Last_Name = txt_Lname.Text;
            s.age = int.Parse(txt_Age.Text);
            s.Dept_id = (int)comp_dept.SelectedValue;
            db.SaveChanges();
            MessageBox.Show("updated successfully!");
            emptyTextBox();
            button1_Click(null, null);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Are you sure you want to delete this record?","Confirmation",MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                student s = db.students.Where(n => n.id == ID).SingleOrDefault();
                db.students.Remove(s);
                db.SaveChanges();
                MessageBox.Show("Deleted successfully!");
                emptyTextBox();
                button1_Click(null, null);
            }
            

        }

        public void emptyTextBox()
        {
            txt_Fname.Text = txt_Lname.Text = txt_Age.Text = "";
            comp_dept.SelectedIndex = -1;
        }


    }
}
