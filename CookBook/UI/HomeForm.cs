using CookBook.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CookBook.UI
{
    public partial class HomeForm : Form
    {
        private readonly IServiceProvider _serviceProvider;
        DesktopFileWatcher _desktopFileWatcher;
        public HomeForm(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _desktopFileWatcher= _serviceProvider.GetRequiredService<DesktopFileWatcher>();
            _desktopFileWatcher.OnFileStatusChanged += OnFileStatusChanged;

            NotificationIcon.Visible = DesktopFileWatcher.CurrentFileStatus;
        }

        private void OnFileStatusChanged(bool fileExists)
        {
            Invoke(new Action(() =>
            {
                NotificationIcon.Visible = fileExists;
            }));
        }

        private void RecipesBtn_Click(object sender, EventArgs e)
        {
            RecipesForm form = _serviceProvider.GetRequiredService<RecipesForm>();
            ShowForm(form);
        }

        private void FridgeBtn_Click(object sender, EventArgs e)
        {
            IngredientsForm form = _serviceProvider.GetRequiredService<IngredientsForm>();
            ShowForm(form);
        }

        private void FoodManagerBtn_Click(object sender, EventArgs e)
        {
            //FoodManagerForm form = _serviceProvider.GetRequiredService<FoodManagerForm>();
            //ShowForm(form);

            ShowForm(_serviceProvider.GetRequiredService<FoodManagerForm>());
        }

        private void ShowForm(Form form)
        {
            //form.StartPosition = FormStartPosition.CenterScreen;
            form.MaximizeBox = false;
            form.MinimizeBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.ShowDialog();
        }
    }
}
