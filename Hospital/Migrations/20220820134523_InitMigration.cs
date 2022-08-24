using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "SEQ_LINEITEM_ID");

            migrationBuilder.CreateSequence(
                name: "SEQ_PATIENT_ID",
                startValue: 1000000L);

            migrationBuilder.CreateSequence(
                name: "SEQ_STAFF_ID",
                startValue: 2000000L);

            migrationBuilder.CreateSequence(
                name: "SEQ_TIMESLOT_ID");

            migrationBuilder.CreateTable(
                name: "DEPARTMENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    BUILDING = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    PHONE = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPARTMENTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MEDICINE",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(9)", maxLength: 9, nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    INVENTORY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    INDICATIONS = table.Column<string>(type: "NVARCHAR2(40)", maxLength: 40, nullable: false),
                    MANUFACTURER = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICINE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PATIENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    GLOBAL_ID = table.Column<string>(type: "NVARCHAR2(18)", maxLength: 18, nullable: false),
                    WARD_ID = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    PASSWORD = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    GENDER = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BIRTHDAY = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    PHONE = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PATIENTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ROOMS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    BUILDING = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    ROOMTYPE = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROOMS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TIME_SLOTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    START_TIME = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    END_TIME = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIME_SLOTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WARDS",
                columns: table => new
                {
                    WARD_ID = table.Column<string>(type: "NVARCHAR2(4)", maxLength: 4, nullable: false),
                    DEPARTMENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BUILDING = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    TYPE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    STARTNUM = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ENDNUM = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WARDS", x => x.WARD_ID);
                    table.ForeignKey(
                        name: "FK_WARDS_DEPARTMENTS_DEPARTMENT_ID",
                        column: x => x.DEPARTMENT_ID,
                        principalTable: "DEPARTMENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ORDERS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    PATIENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    STATE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CREATE_DATE_UTC = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TRANSACTION_METADATA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDERS_PATIENTS_PATIENT_ID",
                        column: x => x.PATIENT_ID,
                        principalTable: "PATIENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SHOPPINGCART",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    PATIENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHOPPINGCART", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SHOPPINGCART_PATIENTS_PATIENT_ID",
                        column: x => x.PATIENT_ID,
                        principalTable: "PATIENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MEDICAL_EQUIPMENTS",
                columns: table => new
                {
                    ID = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    PRODUCER = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    START_USE_TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    ROOM_ID = table.Column<string>(type: "NVARCHAR2(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MEDICAL_EQUIPMENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MEDICAL_EQUIPMENTS_ROOMS_ROOM_ID",
                        column: x => x.ROOM_ID,
                        principalTable: "ROOMS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STAFF",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    GLOBAL_ID = table.Column<string>(type: "NVARCHAR2(18)", maxLength: 18, nullable: false),
                    PASSWORD = table.Column<string>(type: "NVARCHAR2(15)", maxLength: 15, nullable: false),
                    ROLE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    GENDER = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    BIRTHDAY = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    ADDRESS = table.Column<string>(type: "NVARCHAR2(80)", maxLength: 80, nullable: false),
                    PHONE = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false),
                    DEPARTMENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TimeSlotId = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STAFF", x => x.ID);
                    table.ForeignKey(
                        name: "FK_STAFF_DEPARTMENTS_DEPARTMENT_ID",
                        column: x => x.DEPARTMENT_ID,
                        principalTable: "DEPARTMENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_STAFF_TIME_SLOTS_TimeSlotId",
                        column: x => x.TimeSlotId,
                        principalTable: "TIME_SLOTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LINEITEM",
                columns: table => new
                {
                    ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MEDICINE_ID = table.Column<string>(type: "NVARCHAR2(9)", nullable: false),
                    SHOPPINGCART_ID = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    ORDER_ID = table.Column<Guid>(type: "RAW(16)", nullable: true),
                    PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LINEITEM", x => x.ID);
                    table.ForeignKey(
                        name: "FK_LINEITEM_MEDICINE_MEDICINE_ID",
                        column: x => x.MEDICINE_ID,
                        principalTable: "MEDICINE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LINEITEM_ORDERS_ORDER_ID",
                        column: x => x.ORDER_ID,
                        principalTable: "ORDERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LINEITEM_SHOPPINGCART_SHOPPINGCART_ID",
                        column: x => x.SHOPPINGCART_ID,
                        principalTable: "SHOPPINGCART",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PURCHASE_LISTS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    COST = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    STAFF_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    COMMENT = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PURCHASE_LISTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PURCHASE_LISTS_STAFF_STAFF_ID",
                        column: x => x.STAFF_ID,
                        principalTable: "STAFF",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "REGISTRATIONS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    TIME = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    DAY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ORDER = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FEE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PATIENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    STAFF_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ROOM_ID = table.Column<string>(type: "NVARCHAR2(450)", nullable: false),
                    STATE = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    CREATE_DATE_LOCAL = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TRANSACTION_METADATA = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REGISTRATIONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_REGISTRATIONS_PATIENTS_PATIENT_ID",
                        column: x => x.PATIENT_ID,
                        principalTable: "PATIENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REGISTRATIONS_ROOMS_ROOM_ID",
                        column: x => x.ROOM_ID,
                        principalTable: "ROOMS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_REGISTRATIONS_STAFF_STAFF_ID",
                        column: x => x.STAFF_ID,
                        principalTable: "STAFF",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SCHEDULES",
                columns: table => new
                {
                    STAFF_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DAY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TIMESLOT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ROOM_ID = table.Column<string>(type: "NVARCHAR2(450)", nullable: true),
                    CAPACITY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    TOTAL = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SCHEDULES", x => new { x.STAFF_ID, x.DAY });
                    table.ForeignKey(
                        name: "FK_SCHEDULES_ROOMS_ROOM_ID",
                        column: x => x.ROOM_ID,
                        principalTable: "ROOMS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SCHEDULES_STAFF_STAFF_ID",
                        column: x => x.STAFF_ID,
                        principalTable: "STAFF",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SCHEDULES_TIME_SLOTS_TIMESLOT_ID",
                        column: x => x.TIMESLOT_ID,
                        principalTable: "TIME_SLOTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WAITLINES",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    STAFF_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PATIENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    DAY = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ORDER = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WAITLINES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WAITLINES_PATIENTS_PATIENT_ID",
                        column: x => x.PATIENT_ID,
                        principalTable: "PATIENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WAITLINES_STAFF_STAFF_ID",
                        column: x => x.STAFF_ID,
                        principalTable: "STAFF",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PURCHASE_LIST_ITEMS",
                columns: table => new
                {
                    ITEM_ID = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: false),
                    PURCHASE_LIST_ID = table.Column<Guid>(type: "RAW(16)", nullable: false),
                    PURCHASE_LIST_ITEM_TYPE = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    NAME = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ITEM_COUNT = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    PRODUCER = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PURCHASE_LIST_ITEMS", x => new { x.ITEM_ID, x.PURCHASE_LIST_ID });
                    table.ForeignKey(
                        name: "FK_PURCHASE_LIST_ITEMS_PURCHASE_LISTS_PURCHASE_LIST_ID",
                        column: x => x.PURCHASE_LIST_ID,
                        principalTable: "PURCHASE_LISTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DEPARTMENTS",
                columns: new[] { "ID", "BUILDING", "NAME", "PHONE" },
                values: new object[,]
                {
                    { 1, "1号楼", "内科", "11111111111" },
                    { 9, "3号楼", "口腔科", "99999999999" },
                    { 8, "2号楼", "眼科", "88888888888" },
                    { 7, "2号楼", "外科", "77777777777" },
                    { 6, "2号楼", "中医科", "66666666666" },
                    { 10, "3号楼", "妇科", "00000000000" },
                    { 4, "1号楼", "急诊科", "44444444444" },
                    { 3, "1号楼", "皮肤科", "33333333333" },
                    { 2, "1号楼", "儿科", "22222222222" },
                    { 5, "2号楼", "神经科", "55555555555" }
                });

            migrationBuilder.InsertData(
                table: "MEDICINE",
                columns: new[] { "ID", "INDICATIONS", "INVENTORY", "MANUFACTURER", "NAME", "PRICE" },
                values: new object[,]
                {
                    { "H19994016", "有炎症的患者", 500, "昆明贝克诺顿制药有限公司", "阿莫西林克拉维酸钾片", 20.00m },
                    { "Z20040063", "用于治疗流行性感冒属热毒袭肺症", 200, "石家庄以岭药业股份有限公司", "连花清瘟胶囊", 15.00m },
                    { "Z50020615", "用于外感风热所致的咳嗽", 350, "太极集团重庆涪陵制药厂有限公司", "急支糖浆", 25.00m }
                });

            migrationBuilder.InsertData(
                table: "ROOMS",
                columns: new[] { "ID", "BUILDING", "ROOMTYPE" },
                values: new object[,]
                {
                    { "202", "A楼", 2 },
                    { "101", "A楼", 2 },
                    { "102", "A楼", 2 },
                    { "103", "A楼", 2 },
                    { "201", "A楼", 2 },
                    { "203", "A楼", 2 }
                });

            migrationBuilder.InsertData(
                table: "STAFF",
                columns: new[] { "ID", "ADDRESS", "BIRTHDAY", "DEPARTMENT_ID", "GENDER", "GLOBAL_ID", "NAME", "PASSWORD", "PHONE", "ROLE", "TimeSlotId" },
                values: new object[,]
                {
                    { 2000021, "陕西省咸阳市永寿县", new DateTime(1988, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, "665850236429837525", "向紫槐", "RW7fGxEV34d", "93993366799", 0, null },
                    { 2000031, "河北省邢台市柏乡县", new DateTime(1977, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 0, "380234543475013804", "王紫菱", "e_NkkZ9xe^sk", "56644803281", 1, null },
                    { 2000022, "湖北省十堰市勋县", new DateTime(1987, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "304894851618147779", "罗亚梅", "adhk7__dKX", "91167597036", 1, null },
                    { 2000032, "江苏省淮阴市邻水县", new DateTime(1977, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 1, "931807544686001441", "万忆山", "84H0OLa%BQ2!", "25428049805", 1, null },
                    { 2000023, "江苏省连云港市赣榆县", new DateTime(1986, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 0, "986261509125943643", "陆田然", "(7vpTeu!b", "94341610852", 2, null },
                    { 2000033, "安徽省安庆市岳西县", new DateTime(1979, 4, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 0, "679203722177881099", "邹晶辉", "v_yHFYIjn3", "26772684495", 1, null },
                    { 2000024, "湖北省黄冈市罗田县", new DateTime(1981, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, "962041149765665058", "邹萧玉", "w*^RviPyFC", "66558976140", 1, null },
                    { 2000034, "浙江省杭州市淳安县", new DateTime(1968, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 1, "715974750548300271", "祖琴芬", "Oa&r(cWm", "45363277674", 1, null },
                    { 2000025, "陕西省延安市延长县", new DateTime(1977, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 0, "333867359137450567", "姚密如", "fXjpNbQU", "26006740883", 2, null },
                    { 2000035, "贵州省贵阳市修文县", new DateTime(1958, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 0, "215564788365062520", "乌暄莹", "w8QKacFdtfY3", "16246675485", 2, null },
                    { 2000026, "四川省乐山市犍为县", new DateTime(1989, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1, "858384696885017237", "双娴淑", "erCnaFOM%p", "91711269117", 1, null },
                    { 2000036, "广东省韶关市翁源县", new DateTime(1982, 9, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 1, "225994230321642894", "邹南春", "vSw1M0ICnE~8u", "80527942982", 1, null },
                    { 2000027, "陕西省汉中市留坝县", new DateTime(1978, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 0, "481091333354292218", "顾佩兰", "c9XISFcIVhvW", "11119062912", 1, null },
                    { 2000028, "山东省济宁市金乡县", new DateTime(1978, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 1, "714094264311323885", "武灵枫", "zYbL1q~sR", "75857654588", 2, null },
                    { 2000029, "湖南省长沙市宁乡县", new DateTime(1985, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 0, "254709473335247146", "蒲依心", "STI3x~jOQW", "64642789641", 0, null },
                    { 2000030, "山东省临沂市平邑县", new DateTime(1988, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 1, "888034902337818811", "印香卉", "6nH4n(MQr", "86209733191", 1, null }
                });

            migrationBuilder.InsertData(
                table: "PURCHASE_LISTS",
                columns: new[] { "ID", "COMMENT", "COST", "DATE", "STAFF_ID" },
                values: new object[] { new Guid("bdb5a3ab-2173-2650-90b3-00ce06475921"), null, 913m, new DateTime(2022, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 2000021 });

            migrationBuilder.InsertData(
                table: "PURCHASE_LISTS",
                columns: new[] { "ID", "COMMENT", "COST", "DATE", "STAFF_ID" },
                values: new object[] { new Guid("8da78208-5868-fe42-518e-b31dea4c88a5"), "此处为备注", 3000000m, new DateTime(2017, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2000022 });

            migrationBuilder.InsertData(
                table: "PURCHASE_LIST_ITEMS",
                columns: new[] { "ITEM_ID", "PURCHASE_LIST_ID", "ITEM_COUNT", "NAME", "PRICE", "PRODUCER", "PURCHASE_LIST_ITEM_TYPE", "DESCRIPTION" },
                values: new object[] { "H19994016", new Guid("bdb5a3ab-2173-2650-90b3-00ce06475921"), 30, "阿莫西林克拉维酸钾片", 9.6m, "长江制药有限公司", "Medicine", null });

            migrationBuilder.InsertData(
                table: "PURCHASE_LIST_ITEMS",
                columns: new[] { "ITEM_ID", "PURCHASE_LIST_ID", "ITEM_COUNT", "NAME", "PRICE", "PRODUCER", "PURCHASE_LIST_ITEM_TYPE", "DESCRIPTION" },
                values: new object[] { "H20040016", new Guid("bdb5a3ab-2173-2650-90b3-00ce06475921"), 50, "地红霉素肠溶胶囊", 12.5m, "长春制药", "Medicine", null });

            migrationBuilder.InsertData(
                table: "PURCHASE_LIST_ITEMS",
                columns: new[] { "ITEM_ID", "PURCHASE_LIST_ID", "ITEM_COUNT", "NAME", "PRICE", "PRODUCER", "PURCHASE_LIST_ITEM_TYPE", "DESCRIPTION" },
                values: new object[] { "326AG", new Guid("8da78208-5868-fe42-518e-b31dea4c88a5"), 1, "核磁共振成像仪", 3000000m, "GE", "MedicialEquipment", null });

            migrationBuilder.CreateIndex(
                name: "IX_LINEITEM_MEDICINE_ID",
                table: "LINEITEM",
                column: "MEDICINE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LINEITEM_ORDER_ID",
                table: "LINEITEM",
                column: "ORDER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LINEITEM_SHOPPINGCART_ID",
                table: "LINEITEM",
                column: "SHOPPINGCART_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MEDICAL_EQUIPMENTS_ROOM_ID",
                table: "MEDICAL_EQUIPMENTS",
                column: "ROOM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_PATIENT_ID",
                table: "ORDERS",
                column: "PATIENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PURCHASE_LIST_ITEMS_PURCHASE_LIST_ID",
                table: "PURCHASE_LIST_ITEMS",
                column: "PURCHASE_LIST_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PURCHASE_LISTS_STAFF_ID",
                table: "PURCHASE_LISTS",
                column: "STAFF_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATIONS_PATIENT_ID",
                table: "REGISTRATIONS",
                column: "PATIENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATIONS_ROOM_ID",
                table: "REGISTRATIONS",
                column: "ROOM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_REGISTRATIONS_STAFF_ID",
                table: "REGISTRATIONS",
                column: "STAFF_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHEDULES_ROOM_ID",
                table: "SCHEDULES",
                column: "ROOM_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SCHEDULES_TIMESLOT_ID",
                table: "SCHEDULES",
                column: "TIMESLOT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SHOPPINGCART_PATIENT_ID",
                table: "SHOPPINGCART",
                column: "PATIENT_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_DEPARTMENT_ID",
                table: "STAFF",
                column: "DEPARTMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_STAFF_TimeSlotId",
                table: "STAFF",
                column: "TimeSlotId");

            migrationBuilder.CreateIndex(
                name: "IX_WAITLINES_PATIENT_ID",
                table: "WAITLINES",
                column: "PATIENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WAITLINES_STAFF_ID",
                table: "WAITLINES",
                column: "STAFF_ID");

            migrationBuilder.CreateIndex(
                name: "IX_WARDS_DEPARTMENT_ID",
                table: "WARDS",
                column: "DEPARTMENT_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LINEITEM");

            migrationBuilder.DropTable(
                name: "MEDICAL_EQUIPMENTS");

            migrationBuilder.DropTable(
                name: "PURCHASE_LIST_ITEMS");

            migrationBuilder.DropTable(
                name: "REGISTRATIONS");

            migrationBuilder.DropTable(
                name: "SCHEDULES");

            migrationBuilder.DropTable(
                name: "WAITLINES");

            migrationBuilder.DropTable(
                name: "WARDS");

            migrationBuilder.DropTable(
                name: "MEDICINE");

            migrationBuilder.DropTable(
                name: "ORDERS");

            migrationBuilder.DropTable(
                name: "SHOPPINGCART");

            migrationBuilder.DropTable(
                name: "PURCHASE_LISTS");

            migrationBuilder.DropTable(
                name: "ROOMS");

            migrationBuilder.DropTable(
                name: "PATIENTS");

            migrationBuilder.DropTable(
                name: "STAFF");

            migrationBuilder.DropTable(
                name: "DEPARTMENTS");

            migrationBuilder.DropTable(
                name: "TIME_SLOTS");

            migrationBuilder.DropSequence(
                name: "SEQ_LINEITEM_ID");

            migrationBuilder.DropSequence(
                name: "SEQ_PATIENT_ID");

            migrationBuilder.DropSequence(
                name: "SEQ_STAFF_ID");

            migrationBuilder.DropSequence(
                name: "SEQ_TIMESLOT_ID");
        }
    }
}
