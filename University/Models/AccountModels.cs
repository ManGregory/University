using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Mvc;

namespace University.Models
{
    public class UsersContext : DbContext
    {
     //   public UsersContext()
        //    : base("RemoteConnection")

public UsersContext()
           : base("DefaultConnection")



        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ControlType> ControlTypes { get; set; }
        public DbSet<Journal> Journals { get; set; }

        public DbSet<Teacher> Teachers { get; set; }
    }

    public class UniversityContextInitializer : DropCreateDatabaseIfModelChanges<UsersContext>
    {
        private Random r = new Random();

        private string CreateRandomName()
        {
            var names = new List<string>
            {
                "Іван",
                "Петро",
                "Андрій",
                "Інокентій",
                "Олег",
                "Віталій"
            };
            var surnames = new List<string>
            {
                "Іванов",
                "Петров",
                "Кузнєцов",
                "Хоботов",
                "Захаров",
                "Фадєєв"
            };
            var middlenames = new List<string>
            {
                "Іванович",
                "Петрович",
                "Олегович",
                "Віталійович",
                "Олександрович",
                "Андрійович"
            };
            return string.Format("{0} {1} {2}", 
                surnames[r.Next(surnames.Count)],
                names[r.Next(names.Count)],
                middlenames[r.Next(middlenames.Count)]);
        }

        protected override void Seed(UsersContext context)
        {
            base.Seed(context);
            var controlTypes = new List<ControlType>
            {
                new ControlType {Name = "Лабораторна робота 1"},
                new ControlType {Name = "Лабораторна робота 2"},
                new ControlType {Name = "Лабораторна робота 3"},
                new ControlType {Name = "Лабораторна робота 4"},
                new ControlType {Name = "Лабораторна робота 5"},
                new ControlType {Name = "Модуль 1"},
                new ControlType {Name = "Модуль 2"},
                new ControlType {Name = "Контрольна робота 1"},
                new ControlType {Name = "Контрольна робота 2"},
                new ControlType {Name = "Залік"},
                new ControlType {Name = "Іспит"}
            };
            var groups = new List<Group>
            {
                new Group {GroupId = 1, Specialization = "Програмна інженерія"},
                new Group {GroupId = 2, Specialization = "Комп'ютерні системи та мережі"},
                new Group {GroupId = 3, Specialization = "Системи управління автоматикою"}
            };
            var teachers = new List<Teacher>
            {
                new Teacher {TeacherId = 1, Name = "Григоренко Олена Іванівна"},
                new Teacher {TeacherId = 2, Name = "Романюк Юлія Миколаївна"},
                new Teacher {TeacherId = 3, Name = "Бойко Олексій Петрович"},
                new Teacher {TeacherId = 4, Name = "Пєтрова Любов Миколаївна"},
                new Teacher {TeacherId = 5, Name = "Толстой Олексій Костянтинович"},
                new Teacher {TeacherId = 6, Name = "Овдій Михайло Вікторович"},
                new Teacher {TeacherId = 7, Name = "Ковтун Олена Іванівна"}
            };
            var subjects = new List<Subject>
            {
                new Subject {Name = "Програмування на мові С#", TeacherId = 2},
                new Subject {Name = "Програмування на мові Java", TeacherId = 4},
                new Subject {Name = "Основи web-програмування", TeacherId = 6},
                new Subject {Name = "Основи баз даних", TeacherId = 1},
                new Subject {Name = "Організація комп'ютерних мереж", TeacherId = 3},
                new Subject {Name = "Психологія", TeacherId = 5},
                new Subject {Name = "Іноземна мова", TeacherId = 7}
            };
            var students = new List<Student>
            {
                new Student {GroupId = 1, Name = CreateRandomName(), RecordBookNumber = 45678},
                new Student {GroupId = 1, Name = CreateRandomName(), RecordBookNumber = 45785},
                new Student {GroupId = 1, Name = CreateRandomName(), RecordBookNumber = 15478},
                new Student {GroupId = 1, Name = CreateRandomName(), RecordBookNumber = 48796},
                new Student {GroupId = 1, Name = CreateRandomName(), RecordBookNumber = 75395},
                new Student {GroupId = 1, Name = CreateRandomName(), RecordBookNumber = 21345},
                new Student {GroupId = 1, Name = CreateRandomName(), RecordBookNumber = 55555},
                new Student {GroupId = 1, Name = CreateRandomName(), RecordBookNumber = 46547},
                new Student {GroupId = 1, Name = CreateRandomName(), RecordBookNumber = 46879},
                new Student {GroupId = 1, Name = CreateRandomName(), RecordBookNumber = 46878},
                new Student {GroupId = 1, Name = CreateRandomName(), RecordBookNumber = 23146},
                new Student {GroupId = 1, Name = CreateRandomName(), RecordBookNumber = 47899},
                new Student {GroupId = 2, Name = CreateRandomName(), RecordBookNumber = 75585},
                new Student {GroupId = 2, Name = CreateRandomName(), RecordBookNumber = 49988},
                new Student {GroupId = 2, Name = CreateRandomName(), RecordBookNumber = 48795},
                new Student {GroupId = 2, Name = CreateRandomName(), RecordBookNumber = 23146},
                new Student {GroupId = 2, Name = CreateRandomName(), RecordBookNumber = 46565},
                new Student {GroupId = 2, Name = CreateRandomName(), RecordBookNumber = 19546},
                new Student {GroupId = 2, Name = CreateRandomName(), RecordBookNumber = 13458},
                new Student {GroupId = 2, Name = CreateRandomName(), RecordBookNumber = 75954},
                new Student {GroupId = 2, Name = CreateRandomName(), RecordBookNumber = 13654},
                new Student {GroupId = 2, Name = CreateRandomName(), RecordBookNumber = 13659},
                new Student {GroupId = 2, Name = CreateRandomName(), RecordBookNumber = 47891},
                new Student {GroupId = 2, Name = CreateRandomName(), RecordBookNumber = 48775},
                new Student {GroupId = 3, Name = CreateRandomName(), RecordBookNumber = 13255},
                new Student {GroupId = 3, Name = CreateRandomName(), RecordBookNumber = 32165},
                new Student {GroupId = 3, Name = CreateRandomName(), RecordBookNumber = 15649},
                new Student {GroupId = 3, Name = CreateRandomName(), RecordBookNumber = 13545},
                new Student {GroupId = 3, Name = CreateRandomName(), RecordBookNumber = 13884},
                new Student {GroupId = 3, Name = CreateRandomName(), RecordBookNumber = 35445},
                new Student {GroupId = 3, Name = CreateRandomName(), RecordBookNumber = 36897},
                new Student {GroupId = 3, Name = CreateRandomName(), RecordBookNumber = 36974},
                new Student {GroupId = 3, Name = CreateRandomName(), RecordBookNumber = 36954},
                new Student {GroupId = 3, Name = CreateRandomName(), RecordBookNumber = 47885},
                new Student {GroupId = 3, Name = CreateRandomName(), RecordBookNumber = 13354},
                new Student {GroupId = 3, Name = CreateRandomName(), RecordBookNumber = 12358}
            };
            foreach (var controlType in controlTypes)
            {
                context.ControlTypes.Add(controlType);
            }
            foreach (var group in groups)
            {
                context.Groups.Add(group);
            }
            foreach (var teacher in teachers)
            {
                context.Teachers.Add(teacher);
            }
            foreach (var subject in subjects)
            {
                context.Subjects.Add(subject);
            }
            foreach (var student in students)
            {
                context.Students.Add(student);
            }
            context.SaveChanges();

            for (var i = 0; i < 100; i++)
            {
                var journal = new Journal
                {
                    ControlTypeId = controlTypes[r.Next(0, controlTypes.Count)].ControlTypeId,
                    Mark = r.Next(1, 5).ToString(),
                    Name = "Журнал",
                    StudentId = students[r.Next(0, students.Count)].StudentId,
                    SubjectId = subjects[r.Next(0, subjects.Count)].SubjectId
                };
                context.Journals.Add(journal);
            }
            context.SaveChanges();
        }
    }

    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string UserName { get; set; }
    }

    [Table("Group")]
    public class Group
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GroupId { get; set; }
        [DisplayName("Спеціалізація групи")]
        public string Specialization { get; set; }
    }

    [Table("Student")]
    public class Student
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        [DisplayName("Номер залікової")]
        public long RecordBookNumber { get; set; }

        public int? GroupId { get; set; }
        [ForeignKey("GroupId")]
        public Group Group { get; set; }

        [DisplayName("ПІБ студента")]
        public string Name { get; set; }

        [DisplayName("Рік вступу")]
        public string Rik { get; set; }

        [DisplayName("Дата народження")]
        public string DataS { get; set; }

        [DisplayName("Адреса проживання")]
        public string AdrS { get; set; }


        [DisplayName("Контактний телефон")]
        public string TelephoneSt { get; set; }




    }

    [Table("Teacher")]
    public class Teacher
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }
        [DisplayName("ПІБ викладача")]
        public string Name { get; set; }

        [DisplayName("Вчене звання")]
        public string Zvaniya { get; set; }

        [DisplayName("Дата народження")]
        public string DataV { get; set; }

        [DisplayName("Адреса проживання")]
        public string AdrV { get; set; }

        [DisplayName("Контактний телефон")]
        public string TelephoneVikl { get; set; }

    }

    [Table("Subject")]
    public class Subject
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SubjectId { get; set; }
        [Required]
        [Display(Name = "Назва дисципліни")]
        public string Name { get; set; }
        public int? TeacherId { get; set; }
        [ForeignKey("TeacherId")]
        [DisplayName("Викладач")]
        public virtual Teacher Teacher { get; set; }
    }

    [Table("ControlType")]
    public class ControlType
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ControlTypeId { get; set; }
        [DisplayName("Назва виду контролю")]
        public string Name { get; set; }
    }

    [Table("Journal")]
    public class Journal
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int JournalId { get; set; }
        [DisplayName("Найменування")]
        public string Name { get; set; }
        [DisplayName("Оцінка")]
        public string Mark { get; set; }

        public int? ControlTypeId { get; set; }
        [ForeignKey("ControlTypeId")]
        [DisplayName("Найменування контролю")]
        public virtual ControlType ControlType { get; set; }
       
        public int? SubjectId { get; set; }
        [ForeignKey("SubjectId")]
        [DisplayName("Найменування дисципліни")]
        public virtual Subject Subject { get; set; }

        public int? StudentId { get; set; }
        [ForeignKey("StudentId")]
        [DisplayName("ПІБ студента")]
        public virtual Student Student { get; set; }
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "Ім'я користувача")]
        public string UserName { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Поточний пароль")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение \"{0}\" должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Новий пароль")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження пароля")]
        [Compare("NewPassword", ErrorMessage = "Новый пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Ім'я користувача")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запам'ятати мене")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Ім'я користувача")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Значение \"{0}\" должно содержать не менее {2} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Підтвердження пароля")]
        [Compare("Password", ErrorMessage = "Пароль и его подтверждение не совпадают.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
