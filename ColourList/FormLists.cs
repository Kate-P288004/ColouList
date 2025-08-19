using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace MyLists
{
    public partial class FormLists : Form
    {
        // Backing list from repository
        List<Colour> ColourList = ColourRepository.GetAll();

        public FormLists()
        {
            InitializeComponent();
            DisplayList();
            UpdatePreview("#000000"); // safe default if preview panel exists
        }

        // ================= UI Actions =================

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            labelErrorMsg.Text = string.Empty;

            var name = (textBoxInput.Text ?? string.Empty).Trim();
            var hex = GetHexFromTextboxOrDefault();

            if (string.IsNullOrEmpty(name))
            {
                labelErrorMsg.Text = "Enter a colour name.";
                return;
            }
            if (!IsValidHex(hex))
            {
                labelErrorMsg.Text = "Hex must look like #RRGGBB.";
                return;
            }
            if (ColourList.Any(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)))
            {
                labelErrorMsg.Text = "That name already exists.";
                return;
            }

            ColourRepository.Add(name, hex.ToUpperInvariant());
            RefreshBackingList();
            DisplayList();
            textBoxInput.Clear();
            SetHexTextboxText(string.Empty);
            UpdatePreview(hex);
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            labelErrorMsg.Text = string.Empty;

            var idx = listBoxDisplay.SelectedIndex;
            if (idx < 0)
            {
                labelErrorMsg.Text = "Select a colour to edit.";
                return;
            }

            var inputName = (textBoxInput.Text ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(inputName))
            {
                labelErrorMsg.Text = "Type a new name or keep the current one.";
                return;
            }

            // Name clash check (exclude current index)
            for (int i = 0; i < ColourList.Count; i++)
            {
                if (i == idx) continue;
                if (ColourList[i].Name.Equals(inputName, StringComparison.InvariantCultureIgnoreCase))
                {
                    labelErrorMsg.Text = "Another item already has that name.";
                    return;
                }
            }

            var current = ColourList[idx];
            var hex = GetHexFromTextboxOrDefault();
            if (!IsValidHex(hex))
            {
                labelErrorMsg.Text = "Hex must look like #RRGGBB.";
                return;
            }

            ColourRepository.Update(idx, inputName, hex.ToUpperInvariant());
            RefreshBackingList();
            DisplayList();
            UpdatePreview(hex);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            labelErrorMsg.Text = string.Empty;

            var idx = listBoxDisplay.SelectedIndex;
            if (idx < 0)
            {
                labelErrorMsg.Text = "Select a colour to delete.";
                return;
            }

            ColourRepository.RemoveAt(idx);
            RefreshBackingList();
            DisplayList();
            textBoxInput.Clear();
            SetHexTextboxText(string.Empty);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            var term = (textBoxInput.Text ?? string.Empty).Trim();

            if (string.IsNullOrEmpty(term))
            {
                DisplayList();
                return;
            }

            listBoxDisplay.Items.Clear();
            foreach (var c in ColourList)
                if (c.Name.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0)
                    listBoxDisplay.Items.Add(c.ToString());

            labelErrorMsg.Text = listBoxDisplay.Items.Count == 0 ? "No matches." : string.Empty;
        }

        private void ListBoxDisplay_MouseClick(object sender, MouseEventArgs e)
        {
            if (listBoxDisplay.SelectedIndex < 0) return;

            var c = ColourList.ElementAt(listBoxDisplay.SelectedIndex);
            textBoxInput.Text = c.Name;
            SetHexTextboxText(c.Hex);
            UpdatePreview(c.Hex);
        }

        // ===== Designer-wire compatibility (if your .Designer.cs uses these names) =====
        private void ButtonSearch_Click(object sender, EventArgs e) { buttonSearch_Click(sender, e); }
        private void TextBoxInput_MouseDoubleClick(object sender, MouseEventArgs e) { textBoxInput.SelectAll(); }

        // ================= Helpers =================

        private void DisplayList()
        {
            listBoxDisplay.Items.Clear();
            foreach (var c in ColourList)
                listBoxDisplay.Items.Add(c.ToString());
            labelErrorMsg.Text = string.Empty;
        }

        private void RefreshBackingList()
        {
            ColourList = ColourRepository.GetAll();
        }

        // If you added a TextBox named textBoxHex and a Panel named panelPreview in Designer,
        // these helpers will use them. If not, they safely no-op.

        private string GetHexFromTextboxOrDefault()
        {
            if (textBoxHex != null && !string.IsNullOrWhiteSpace(textBoxHex.Text))
                return textBoxHex.Text.Trim();
            return "#000000";
        }

        private void SetHexTextboxText(string s)
        {
            if (textBoxHex != null) textBoxHex.Text = s ?? string.Empty;
        }

        private bool IsValidHex(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length != 7) return false;
            if (s[0] != '#') return false;
            for (int i = 1; i < 7; i++)
            {
                char ch = s[i];
                bool digit = ch >= '0' && ch <= '9';
                bool upper = ch >= 'A' && ch <= 'F';
                bool lower = ch >= 'a' && ch <= 'f';
                if (!(digit || upper || lower)) return false;
            }
            return true;
        }

        private void UpdatePreview(string hex)
        {
            try
            {
                if (panelPreview == null) return;
                int r = Convert.ToInt32(hex.Substring(1, 2), 16);
                int g = Convert.ToInt32(hex.Substring(3, 2), 16);
                int b = Convert.ToInt32(hex.Substring(5, 2), 16);
                panelPreview.BackColor = System.Drawing.Color.FromArgb(r, g, b);
            }
            catch { /* ignore bad values */ }
        }

        private void textBoxHex_TextChanged(object sender, EventArgs e)
        {
            var hex = (textBoxHex.Text ?? string.Empty).Trim();
            if (IsValidHex(hex)) UpdatePreview(hex);
        }
    }
}
