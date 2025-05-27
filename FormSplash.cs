using System;
using System.Windows.Forms;

namespace DXApplication1.Myforms
{
    public partial class FormSplash : Form
    {
        // 定义一个索引变量，用于跟踪当前需要显示的 Label
        private int labelIndex = 0;
        // 定义一个数组，存储所有需要逐个显示的 Label
        private Label[] labels;
        public FormSplash()
        {
            InitializeComponent();
            // 初始化 Label 数组
            labels = new Label[] { lblMsg1, lblMsg2, lblMsg3 }; // 根据实际情况添加更多 Label

            // 隐藏所有 Label
            foreach (var label in labels)
            {
                label.Visible = false;
            }

            // 启动 Timer
            timer1.Interval = 1000; // 设置间隔为 1 秒
            timer1.Start();
        }

        private void FormSplash_Load(object sender, EventArgs e)
        {
        }

        // 检测数据库
        private void FormSplash_Shown(object sender, EventArgs e)
        {
            this.Refresh();
            this.lblMsg1.Text = "数据库连接成功，准备登录检测...";
            this.lblMsg1.Refresh();
        }

        // 时钟到时发生的事件
        private void timer1_Tick(object sender, EventArgs e)
        {
            // 检查是否还有 Label 需要显示
            if (labelIndex < labels.Length)
            {
                // 显示当前 Label
                labels[labelIndex].Visible = true;

                // 更新索引
                labelIndex++;
            }
            else
            {
                // 所有 Label 都已显示，停止 Timer
                timer1.Stop();
                // 跳转到登录窗体
                ShowLoginForm();
            }
        }

        // 显示登录窗体的方法
        private void ShowLoginForm()
        {
            Myforms.FormLogin frm = new FormLogin();
            frm.ShowDialog();
            // 登录窗体关闭后，关闭当前窗体
            this.Close();
        }
    }
}