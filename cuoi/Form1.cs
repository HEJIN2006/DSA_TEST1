using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cuoi
{
    public partial class Form1 : Form
    {
        // Định nghĩa Node cho LinkedList
        class PostNode
        {
            public int ID; // ID bài viết
            public string UserName; // Tên người đăng
            public string Content; // Nội dung bài viết
            public PostNode Next; // Trỏ đến bài viết tiếp theo

            public PostNode(int id, string user, string content)
            {
                ID = id;
                UserName = user;
                Content = content;
                Next = null;
            }
        }

        private PostNode head = null; // Điểm bắt đầu của LinkedList
        private int nextID = 1; // ID tự động tăng

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Không cần Console.ReadLine(), chỉ lấy nội dung từ TextBox
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // Không cần Console.ReadLine(), chỉ lấy tên từ TextBox
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            // Không cần Console.ReadLine(), chỉ lấy ID từ TextBox
            if (!string.IsNullOrWhiteSpace(textBox3.Text) && !int.TryParse(textBox3.Text, out _))
            {
                MessageBox.Show(" ID phải là một số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.Clear(); // Xóa nội dung nhập sai
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = txtTenNguoiDang.Text;
            string content = txtNoiDung.Text;

            if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(content))
            {
                // Tạo một bài viết mới trong LinkedList
                PostNode newPost = new PostNode(nextID, userName, content);
                nextID++; // Tăng ID cho bài đăng tiếp theo

                if (head == null)
                {
                    head = newPost;
                }
                else
                {
                    PostNode temp = head;
                    while (temp.Next != null)
                    {
                        temp = temp.Next;
                    }
                    temp.Next = newPost;
                }

                // Hiển thị lại danh sách bài viết
                DisplayPosts();

                // Xóa nội dung nhập vào
                txtTenNguoiDang.Clear();
                txtNoiDung.Clear();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (int.TryParse(textBox3.Text, out int id))
            {
                if (head == null) return;

                if (head.ID == id)
                {
                    head = head.Next; // Xóa bài viết đầu tiên
                }
                else
                {
                    PostNode temp = head;
                    while (temp.Next != null && temp.Next.ID != id)
                    {
                        temp = temp.Next;
                    }
                    if (temp.Next != null)
                    {
                        temp.Next = temp.Next.Next;
                    }
                }

                DisplayPosts();
                textBox3.Clear();
            }
        }

        private void DisplayPosts()
        {
            listBox1.Items.Clear(); // Xóa danh sách cũ

            PostNode temp = head;
            while (temp != null)
            {
                listBox1.Items.Add($"ID: {temp.ID} | {temp.UserName}: {temp.Content}");
                temp = temp.Next;
            }
        }

        // Các sự kiện Label không cần xử lý gì
        private void label1_Click(object sender, EventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
        private void label4_Click(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }
        private void label8_Click(object sender, EventArgs e) { }
        private void label11_Click(object sender, EventArgs e) { }
        private void label13_Click(object sender, EventArgs e) { }
        private void label15_Click(object sender, EventArgs e) { }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Khởi tạo danh sách bài viết nếu cần
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string searchText = textBox3.Text.Trim();
            if (string.IsNullOrWhiteSpace(searchText)) return;

            listBox1.Items.Clear(); // Xóa danh sách cũ trước khi hiển thị kết quả tìm kiếm
            PostNode temp = head;
            bool found = false;

            while (temp != null)
            {
                if (int.TryParse(searchText, out int searchID) && temp.ID == searchID)
                {
                    // Tìm theo ID
                    listBox1.Items.Add($"ID: {temp.ID} | {temp.UserName}: {temp.Content}");
                    found = true;
                    break; // Vì ID là duy nhất, không cần tìm tiếp
                }
                else if (temp.UserName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    // Tìm theo tên người đăng (không phân biệt hoa thường)
                    listBox1.Items.Add($"ID: {temp.ID} | {temp.UserName}: {temp.Content}");
                    found = true;
                }
                temp = temp.Next;
            }

            if (!found)
            {
                listBox1.Items.Add("❌ Không tìm thấy bài viết!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DisplayPosts(); // Gọi lại hàm hiển thị danh sách đầy đủ
            textBox3.Clear();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox3.Text, out int postId))
            {
                MessageBox.Show("⚠️ ID phải là một số!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newUserName = txtTenNguoiDang.Text.Trim();
            string newContent = txtNoiDung.Text.Trim();

            if (string.IsNullOrWhiteSpace(newUserName) || string.IsNullOrWhiteSpace(newContent))
            {
                MessageBox.Show("⚠️ Vui lòng nhập đầy đủ tên người đăng và nội dung!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PostNode temp = head;
            bool found = false;

            while (temp != null)
            {
                if (temp.ID == postId)
                {
                    temp.UserName = newUserName;
                    temp.Content = newContent;
                    found = true;
                    break;
                }
                temp = temp.Next;
            }

            if (found)
            {
                MessageBox.Show("✅ Bài đăng đã được cập nhật!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayPosts(); // Cập nhật danh sách hiển thị
                textBox3.Clear();
                txtTenNguoiDang.Clear();
                txtNoiDung.Clear();
            }
            else
            {
                MessageBox.Show("❌ Không tìm thấy bài đăng có ID này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
