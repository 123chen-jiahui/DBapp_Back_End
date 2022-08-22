using Hospital.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Hospital.Database
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {


        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Ward> Wards { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Break> Breaks { get; set; }
        public DbSet<Resign> Resigns { get; set; }
        public DbSet<MedicalEquipment> MedicalEquipments { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<PurchaseList> PurchaseLists { get; set; }
        public DbSet<PurchaseListItem> PurchaseListItems { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<WaitLine> WaitLines { get; set; }
        // public DbSet<Staff_TimeSlot> Staff_TimeSlots { get; set; }
        // public DbSet<Staff_Room> Staff_Rooms { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasSequence("SEQ_PATIENT_ID")
                .StartsAt(1000000)
                .IncrementsBy(1);
            // modelBuilder.HasDefaultSchema("C##TEST");
            modelBuilder.HasSequence("SEQ_STAFF_ID")
                .StartsAt(2000000)
                .IncrementsBy(1);
            modelBuilder.HasSequence("SEQ_LINEITEM_ID")
                .StartsAt(1)
                .IncrementsBy(1);
            modelBuilder.HasSequence("SEQ_TIMESLOT_ID")
                .StartsAt(1)
                .IncrementsBy(1);
            modelBuilder.Entity<Patient>(entity =>
            {
                entity.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .UseHiLo("SEQ_PATIENT_ID");
            });
            modelBuilder.Entity<Staff>(entity =>
            {
                entity.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .UseHiLo("SEQ_STAFF_ID");
            });
            modelBuilder.Entity<LineItem>(entity =>
            {
                entity.Property(o => o.Id)
                .ValueGeneratedOnAdd()
                .UseHiLo("SEQ_LINEITEM_ID");
            });
            modelBuilder.Entity<TimeSlot>(entity =>
            {
                entity.Property(ts => ts.Id)
                .ValueGeneratedOnAdd()
                .UseHiLo("SEQ_TIMESLOT_ID");
            });
            modelBuilder.Entity<Schedule>()
                .HasKey(st => new { st.StaffId, st.Day });
            /*modelBuilder.Entity<Staff_TimeSlot>()
                .HasKey(st => new { st.StaffId, st.Day });*/
            // 添加科室数据
            // 读取json数据
            var departmentJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Database/DepartmentMockData.json"); // 字符串前加@表示这是一个C#的string，不过我们还要获得当前项目的文件夹地址，需要用到C#的反射机制
            // 由json对象转换为数据类型，需要Newtonsoft.Json包来处理
            IList<Department> departments = JsonConvert.DeserializeObject<IList<Department>>(departmentJsonData);
            modelBuilder.Entity<Department>().HasData(departments);

            // 添加药品数据
            var medicineJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Database/MedicineMockData.json");
            IList<Medicine> medicine = JsonConvert.DeserializeObject<IList<Medicine>>(medicineJsonData);
            modelBuilder.Entity<Medicine>().HasData(medicine);

            // 添加员工数据
            var staffJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Database/StaffMockData.json");
            IList<Staff> staff = JsonConvert.DeserializeObject<IList<Staff>>(staffJsonData);
            modelBuilder.Entity<Staff>().HasData(staff);

            // 添加房间数据
            var roomJsonData = File.ReadAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"/Database/RoomMockData.json");
            IList<Room> room = JsonConvert.DeserializeObject<IList<Room>>(roomJsonData);
            modelBuilder.Entity<Room>().HasData(room);
            // modelBuilder.HasSequence("SEQ_PATIENT_ID", "C##TEST").IncrementsBy(1);




            modelBuilder.Entity<Break>()
             .HasOne(u => u.Staff)
             .WithMany(u => u.Breaks_doctor)
             .HasForeignKey(s => s.StaffId)
             .OnDelete(DeleteBehavior.Restrict);
       
            modelBuilder.Entity<Break>()
              .HasOne(u => u.Admin)
              .WithMany(u => u.Breaks_admin)
              .HasForeignKey(s => s.AdminId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Resign>()
             .HasOne(u => u.Admin)
             .WithMany(u => u.Resign_admin)
             .HasForeignKey(s => s.AdminId)
             .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Resign>()
              .HasOne(u => u.Staff)
              .WithMany(u => u.Resign_doctor)
              .HasForeignKey(s => s.StaffId)
              .OnDelete(DeleteBehavior.Restrict);
            base.OnModelCreating(modelBuilder);
        }
    }
}
