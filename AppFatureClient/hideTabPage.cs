using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFatureClient
{
    public class hideTabPage
    {
        int Index1, Index2;
        //Remove a TabPage
        public void HideTabPage(System.Windows.Forms.TabPage tp, System.Windows.Forms.TabControl tc)
        {
            if (tc.TabPages.Contains(tp))
            {
                tc.TabPages.Remove(tp);
            }
        }
        //Show TabPage
        public void ShowTabPage(System.Windows.Forms.TabPage tp, System.Windows.Forms.TabControl tc)
        {
            ShowTabPage(tp, tc.TabPages.Count, tc);
        }
        
        public void ShowTabPage(System.Windows.Forms.TabPage tp, int index, System.Windows.Forms.TabControl tc)
        {
            if (tc.TabPages.Contains(tp))
            {
                return;
            }
            InsertTabPage(tp, index, tc);
        }
        //Adiciona a TabPage
        private void InsertTabPage(System.Windows.Forms.TabPage tabpage, int index, System.Windows.Forms.TabControl tc)
        {
            if (index < 0 || index > tc.TabCount)
            {
                throw new ArgumentException("Index out of Range");
            }
            tc.TabPages.Add(tabpage);
            if (index < tc.TabCount -1)
            {
                while (tc.TabPages.IndexOf(tabpage) != index)
                {
                    SwapTabPages(tabpage, (tc.TabPages[tc.TabPages.IndexOf(tabpage) - 1]), tc);
                }
            }
            tc.SelectTab(tabpage);
        }
        //Troca das TabPage
        private void SwapTabPages(System.Windows.Forms.TabPage tp1, System.Windows.Forms.TabPage tp2, System.Windows.Forms.TabControl tc)
        {
            if (tc.TabPages.Contains(tp1).Equals(false) || tc.TabPages.Contains(tp2).Equals(false))
            {
                throw new ArgumentException("TabPages tem que estar dentro da TabControl --> TabPageCollenction");
            }
            Index1 = tc.TabPages.IndexOf(tp1);
            Index2 = tc.TabPages.IndexOf(tp2);
            tc.TabPages[Index1] = tp2;
            tc.TabPages[Index2] = tp1;
        }
    }
}
