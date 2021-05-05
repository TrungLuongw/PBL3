create database PBL
go
use PBL
go
create table ThoiGian
(
	id int primary key,
	Time nvarchar(20)
)
create table Ngay
(
	id int primary key,
	Thu nvarchar(20)
)
go
go
go
go

create table TTKH
(
	id int identity primary key,
	ten nvarchar(40) not null,
	sdt nvarchar(12) not null,
	ngaysinh date,
	diachi nvarchar(40),
	cmnd nvarchar(10)

)
create table GoiTap
(
	id int identity primary key,
	ten nvarchar(30),
	mota nvarchar,
	sobuoi int,
	giatien float

)
create table Status
(
id int primary key,
ten nvarchar(30)
)
create table Chucvu
(
	id int primary key,
	chucvu nvarchar(20)
	)
go
go
go
go
create table TTNV
(
	id int identity primary key,
	ten nvarchar(30),
	sdt nvarchar(12),
	ngaysinh date,
	diachi nvarchar(40),
	cmnd nvarchar(10),
	ngayvao date,
	ngayra date , 
	idchucvu int
	foreign key (idchucvu) references Chucvu(id)
)

	create table taikhoan
	(
		id varchar(40) primary key,
		idnv int ,
		pass varchar(40)
		foreign key (idnv) references TTNV(id)
		)
		go
	create table PT
	(
	id int identity primary key,
	idnv int ,
	soluonghd int
	foreign key (idnv) references TTNV(id)
	)
	go
	
	go
create table Hopdong
(
	id int identity primary key,
	idKH int,
	idgoitap int ,
	idstatus int ,
	ngayki date ,
	ngayhethan date,
	idpt int
	foreign key (idKH) references TTKH(id),
	foreign key (idgoitap) references Goitap(id),
	foreign key (idstatus) references Status(id),
	foreign key (idpt) references PT(id)
	)
	go
	create table lichtap
	(
		id int identity primary key,
		idhopdong int ,
		idngay int,
		idtime int
		foreign key (idhopdong) references Hopdong(id),
		foreign key (idngay) references Ngay(id),
		foreign key (idtime) references Thoigian(id)

	)
insert ThoiGian values(1,'3:00')
insert ThoiGian values(2,'3:30')
insert ThoiGian values(3,'4:00')
insert ThoiGian values(4,'4:30')
insert ThoiGian values(5,'5:00')
insert ThoiGian values(6,'5:30')
insert ThoiGian values(7,'6:00')
insert ThoiGian values(8,'6:30')
insert ThoiGian values(9,'7:00')
insert ThoiGian values(10,'7:30')
insert ThoiGian values(11,'8:00')
insert ThoiGian values(12,'8:30')
insert ThoiGian values(13,'9:00')
insert ThoiGian values(14,'9:30')
insert ThoiGian values(15,'10:00')
insert ThoiGian values(16,'10:30')
insert ThoiGian values(17,'11:00')
insert ThoiGian values(18,'11:30')
insert ThoiGian values(19,'12:00')
insert ThoiGian values(20,'12:30')
insert ThoiGian values(21,'13:00')
insert ThoiGian values(22,'13:30')
insert ThoiGian values(23,'14:00')
insert ThoiGian values(24,'14:30')
insert ThoiGian values(25,'15:00')
insert ThoiGian values(26,'15:30')
insert ThoiGian values(27,'16:00')
insert ThoiGian values(28,'16:30')
insert ThoiGian values(29,'17:00')
insert ThoiGian values(30,'17:30')
insert ThoiGian values(31,'18:00')
insert ThoiGian values(32,'18:30')
insert ThoiGian values(33,'19:00')
insert ThoiGian values(34,'19:30')
insert ThoiGian values(35,'20:00')
insert ThoiGian values(36,'20:30')
insert ThoiGian values(37,'21:00')
insert ThoiGian values(38,'21:30')
insert ThoiGian values(39,'22:00')
insert ThoiGian values(40,'22:30')
insert ThoiGian values(41,'23:00')
insert ThoiGian values(42,'23:30')

go
insert Ngay values(1,N'Thứ 2')
insert Ngay values(2,N'Thứ 3')
insert Ngay values(3,N'Thứ 4')
insert Ngay values(4,N'Thứ 5')
insert Ngay values(5,N'Thứ 6')
insert Ngay values(6,N'Thứ 7')
insert Ngay values(7,N'Chủ nhật')

go
insert Chucvu values(1,N'Admin')
insert Chucvu values(2,N'Staff')
go
--ALTER TABLE Goitap
--ALTER COLUMN mota nvarchar(100)
--set identity_insert Goitap on;
--insert into GoiTap(id,ten,mota,sobuoi,giatien) values (0,N'Không có',N'không có',0,0.0)
--set identity_insert Goitap off



select Thu as [Thứ],count(TTKH.ten) as [số ca] from Hopdong,lichtap,ThoiGian,Ngay,TTKH,PT,TTNV
where lichtap.idhopdong = Hopdong.id and ThoiGian.id= lichtap.idtime and Ngay.id = lichtap.idngay and Hopdong.idKH = TTKH.id and pt.idnv = TTNV.id and Hopdong.idpt = PT.id 
group by Ngay.Thu
select ThoiGian.Time , TTNV.ten as tennv,TTKH.ten as tenkh from Hopdong,lichtap,ThoiGian,TTKH,PT,TTNV
where Hopdong.id = lichtap.idhopdong and lichtap.idtime =ThoiGian.id and PT.idnv = TTNV.id and Hopdong.idpt = pt.id and TTKH.id = Hopdong.idKH and lichtap.idngay = 1
select * from Status
select ThoiGian.Time , Count(ThoiGian.Time) from lichtap,ThoiGian,Ngay
where lichtap.idtime = ThoiGian.id and lichtap.idngay = Ngay.id and Ngay.id = 4
group by ThoiGian.Time
select * from GoiTap
select * from Hopdong
select * from lichtap 
select * from Ngay
select * from PT
select * from Status 
select * from ThoiGian
select * from TTKH
select * from TTNV
