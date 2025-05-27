using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DXApplication1.Myforms
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }
        private SqlConnection objeconnect;

        //登录窗体加载事件
        private void FormLogin_Load(object sender, EventArgs e)
        {
            objeconnect = new SqlConnection("server=127.0.0.1;uid=sa;pwd=root;database=Admin");
        }


        //通过取消按钮来控制窗体关闭
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //登录按钮点击事件
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 获取用户输入的用户名和密码
            string username = txtUser.Text.Trim();
            string password = txtPassword.Text.Trim();

            // 检查用户名和密码是否为空
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("用户名或密码不能为空！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 打开数据库连接(保持在登录的过程中，数据库保持打开的状态)
            try
            {
                objeconnect.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据库连接失败，请检查网络或配置！\n" + ex.Message, "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            // 查询数据库中是否存在该用户
            string query = "SELECT COUNT(*) FROM Login WHERE 账号 = @UserName AND 密码 = @Password";
            using (SqlCommand cmd = new SqlCommand(query, objeconnect))
            {
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Password", password);

                int result = (int)cmd.ExecuteScalar();

                if (result > 0)
                {
                    // 登录成功，跳转到主窗体
                    this.Hide();
                    FormMain frmMain = new FormMain();
                    frmMain.ShowDialog();
                    this.Close();
                }
                else
                {
                    // 登录失败，提示用户
                    MessageBox.Show("用户名或密码错误！", "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //数据库连接测试按钮点击事件
        private void buttonTest_Click(object sender, EventArgs e)
        {
            try
            {
                objeconnect.Open();
                MessageBox.Show("连接成功");
                objeconnect.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

       
    }
}
