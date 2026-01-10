using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ChatApp_Client
{
    public partial class frmCreateGroup : Form
    {
        // tên user hiện tại (để auto thêm vào nhóm)
        private readonly string _currentUser;

        // Event báo ngược về frmClient khi tạo nhóm xong
        // string: tên nhóm, List<string>: danh sách members
        public event Action<string, List<string>> GroupCreated;

        public frmCreateGroup()
        {
            InitializeComponent();
        }

        public frmCreateGroup(string currentUser) : this()
        {
            _currentUser = currentUser;
        }


        private void btnCreate_Click(object sender, EventArgs e)
        {
            string groupName = txtGroupName.Text.Trim();
            string membersRaw = txtMembers.Text.Trim();   // ví dụ: "Sang, Thanh, Phong"

            if (string.IsNullOrEmpty(groupName))
            {
                MessageBox.Show("Vui lòng nhập tên nhóm.",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtGroupName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(membersRaw))
            {
                MessageBox.Show("Vui lòng nhập thành viên (cách nhau bởi dấu phẩy).",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMembers.Focus();
                return;
            }


            var members = membersRaw
                .Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(m => m.Trim())
                .Where(m => !string.IsNullOrEmpty(m))
                .Distinct()
                .ToList();

            // Đảm bảo người tạo cũng là member
            if (!members.Contains(_currentUser))
                members.Add(_currentUser);

            // Bắn event cho frmClient xử lý (gửi lên server)
            GroupCreated?.Invoke(groupName, members);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
