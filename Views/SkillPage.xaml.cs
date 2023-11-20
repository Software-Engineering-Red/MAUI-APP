using UndacApp.Models;
using UndacApp.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;


namespace UndacApp.Views
{
    public partial class SkillPage : ContentPage
    {
        Skill selectedSkill = null;
        ISkillService skillService;
        ObservableCollection<Skill> skills = new ObservableCollection<Skill>();

        public SkillPage()
        {
            InitializeComponent();
            BindingContext = new Skill();
            this.skillService = new SkillService();

            Task.Run(async () => await LoadSkills());
            txe_skill.Text = "";
        }

        private async Task LoadSkills()
        {
            skills = new ObservableCollection<Skill>(await skillService.GetAll());
            ltv_skills.ItemsSource = skills;
        }

        private void SaveButton_Clicked(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txe_skill.Text)) return;

            if (selectedSkill == null)
            {
                var skill = new Skill() { Name = txe_skill.Text };
                skillService.Add(skill);
                skills.Add(skill);
            }
            else
            {
                selectedSkill.Name = txe_skill.Text;
                skillService.Remove(selectedSkill);
                var skill = skills.FirstOrDefault(x => x.ID == selectedSkill.ID);
                skill.Name = txe_skill.Text;
            }

            selectedSkill = null;
            ltv_skills.SelectedItem = null;
            txe_skill.Text = "";

        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            if (ltv_skills.SelectedItem == null)
            {
                await DisplayAlert("No Skill Selected", "Select the skill you want to delete from the list", "OK");
                return;
            }

            await skillService.Remove(selectedSkill);
            skills.Remove(selectedSkill);

            ltv_skills.SelectedItem = null;
            txe_skill.Text = "";
        }

        private void ltv_skills_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            selectedSkill = e.SelectedItem as Skill;
            if (selectedSkill == null) return;

            txe_skill.Text = selectedSkill.Name;
        }
    }
}
