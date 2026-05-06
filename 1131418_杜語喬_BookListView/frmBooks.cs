using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _1131418_杜語喬_BookListView
{
    public partial class frmBooks : Form
    {
        string[] b_name = { "賣瓜的人", "財富階梯", "心理操盤術", "北歐時間", "脆弱的力量", "古書食堂", "正念解憂的佛貓", "單身經濟學" }; //書名
        string[] author = { "作者： 洪倪", "作者： 尼克．馬朱利 ", "作者： 馬丁．普林", "作者： 日暮Inko", "作者：布芮尼．布朗 ", "作者：原田比香 ", "作者： 龍悠, 朗諾德•史威普", "作者： 崔英勝, 吳為" }; //作者
        string[] kind = { "出版社：遠流", "出版社：商業周刊", "出版社：樂金文化", "出版社：幸福文化", "出版社：馬可孛羅 ", "出版社：圓神", "出版社：方言文化", "出版社：財經錢線文化有限公司" }; //出版社
        public frmBooks()
        {
            InitializeComponent();

        }

        private void frmBooks_Load(object sender, EventArgs e)
        {
            cmbView.Items.Add("大圖示");
            cmbView.Items.Add("詳細資料");
            cmbView.Items.Add("小圖示");
            cmbView.Items.Add("清單");
            cmbView.Items.Add("大圖示加詳細資料");
            cmbView.SelectedIndex = 0; //預設選取第一個項目
            lvwBooks.Columns.Add("書名", 130); //新增 書名 欄位，寬度為100
            lvwBooks.Columns.Add("作者", 200); //新增 作者 欄位，寬度為60
            lvwBooks.Columns.Add("類別", 200); //新增 類別 欄位
            lvwBooks.BeginUpdate(); //暫停重繪
            for (int i = 0; i < b_name.Length; i++)
            { //宣告一個ListViewItem物件
                ListViewItem lvi = new ListViewItem(b_name[i]);
                lvi.SubItems.Add(author[i].ToString()); //新增 作者 欄位資料
                lvi.SubItems.Add(kind[i]); //新增 類別 欄位資料
                lvwBooks.Items.Add(lvi); //新增項目
                lvwBooks.Items[i].ImageIndex = i; //指定影像的索引值
            }
            lvwBooks.EndUpdate(); //重繪;
        }

        private void cmbView_SelectedIndexChanged(object sender, EventArgs e)
        {
            //根據cmbView的SelectedIndex屬性值，設定檢視方式
            switch (cmbView.SelectedIndex)
            {
                case 0: //大圖示
                    lvwBooks.View = View.LargeIcon;
                    break;
                case 1: //詳細資料
                    lvwBooks.View = View.Details;
                    break;
                case 2: //小圖示
                    lvwBooks.View = View.SmallIcon;
                    break;
                case 3: //清單
                    lvwBooks.View = View.List;
                    break;
                case 4: //大圖示加詳細資料
                    lvwBooks.View = View.Tile;
                    break;
            }
        }

        private void lvwBooks_ItemActivate(object sender, EventArgs e)
        {
            //取得書名
            string strBookname = b_name[lvwBooks.SelectedIndices[0]];
            bool exist = lstBorrow.Items.Contains(strBookname);
            
            if (!exist) // 若選取的書名不存在借書清單中
            {
                DialogResult dr = MessageBox.Show("確定要借閱嗎?",
                strBookname, MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes) // 若按 <是> 鈕
                { // 新增項目到借書清單
                    lstBorrow.Items.Add(strBookname);
                }
            }
            else // 若書籍已存在借書清單中
            {
                DialogResult dr = MessageBox.Show("確定要還書嗎?",
                strBookname, MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes) // 若按 <是> 鈕
                { // 移除項目從借書清單
                    lstBorrow.Items.Remove(strBookname);
                }
            }
        }

        private void lvwBooks_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmBooks_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 詢問使用者是否確認要離開
            DialogResult dr = MessageBox.Show("確定要離開應用程式嗎?",
                "確認離開", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.No) // 若按 <否> 鈕
            {
                e.Cancel = true; // 取消關閉視窗
            }
        }
    }
}
