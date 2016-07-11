using ProjectManager.dao;
using ProjectManager.dto;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ProjectManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void dgvProjects_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            Project currentProject=(Project)dgvProjects.SelectedItem;
            lblProjectName.Content =currentProject.Name;
            dto.Task[] tasks = TaskDAO.getAllTasksOnProject(currentProject.Id);
            dgvTasks.ItemsSource = tasks;
            lblProjectStartDate.Content = currentProject.StartDate.ToString();
            lblProjectEndDate.Content = currentProject.EndDate.ToString();
            lblProjectDeadLine.Content = currentProject.DeadLine.ToString();
            lblProjectStatus.Content = currentProject.Status;
            lblProjectNote.Content = currentProject.Note;
            dgvTasks.SelectedIndex = 0;
        }

        private void btnTaskDetails_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1;
            //load first task of selected proejct
            dto.Task currentTask = (dto.Task)dgvTasks.SelectedItem;
            lblTaskName.Content = currentTask.Name;
            lblTaskStartDate.Content = currentTask.StartDate;
            lblTaskEndDate.Content = currentTask.EndDate;
            lblTaskDeadLine.Content = currentTask.DeadLine;
            lblTaskManHours.Content = currentTask.ManHours;
            lblTaskPercentage.Content = currentTask.Percentage.ToString();
            lblTaskNote.Content = currentTask.Note;
            //load users on task
            TaskUser[] users = TaskUserDAO.getAllUsersOnTask(currentTask.Id);
            dgvUsersOnTask.ItemsSource = users;
        }

        private void btnProjectDetails_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 0;
        }

        private void frmProjectManager_Loaded(object sender, RoutedEventArgs e)
        {
            //load projects
            Project[] projects = ProjectDAO.getAllProjects();
            dgvProjects.ItemsSource = projects;
            Project currentProject = (Project)dgvProjects.Items[0];
            //load tasks
            dto.Task[] tasks = TaskDAO.getAllTasksOnProject(currentProject.Id);
            dgvTasks.ItemsSource = tasks;
            lblProjectStartDate.Content = currentProject.StartDate.ToString();
            lblProjectEndDate.Content = currentProject.EndDate.ToString();
            lblProjectDeadLine.Content = currentProject.DeadLine.ToString();
            lblProjectStatus.Content = currentProject.Status;
            lblProjectNote.Content = currentProject.Note;
            dgvProjects.SelectedIndex = 0;
            dgvTasks.SelectedIndex = 0;
            //load project users
            ProjectUser[] projectUsers = ProjectUserDAO.getAllUsersOnProject(currentProject.Id);
            dgvUsersOnProject.ItemsSource = projectUsers;
            //load project incomes
            Income[] incomes = IncomeDAO.getAllIncomesOnProject(currentProject.Id);
            double amount = 0;
            foreach (Income income in incomes)
            {
                amount += income.Amount;
            }
            lblProjectIncomes.Content = amount.ToString() + " KM";
            //load project expences
            Expence[] expences = ExpenceDAO.getAllIncomesOnProject(currentProject.Id);
            amount = 0;
            foreach (Expence expence in expences)
            {
                amount += expence.Amount;
            }
            lblProjectExpences.Content = amount.ToString() + " KM";
        }

    }
}
