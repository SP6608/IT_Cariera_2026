create database health_records;
use health_records;

create table Patient(
PatientId INT PRIMARY KEY AUTO_INCREMENT,
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
DateOfBirth DATE NOT NULL,
Gender NVARCHAR(50) NOT NULL,
PhoneNumber NVARCHAR(15),
Email NVARCHAR(100),
Address NVARCHAR(255),
EmergencyContact NVARCHAR(100)
);

create table Doctor(
DoctorId INT PRIMARY KEY AUTO_INCREMENT,
FirstName NVARCHAR(50) NOT NULL,
LastName NVARCHAR(50) NOT NULL,
Specialization NVARCHAR(100) NOT NULL,
PhoneNumber NVARCHAR(15),
Email NVARCHAR(100)
);

create table MedicalRecord(
RecordId INT PRIMARY KEY AUTO_INCREMENT,
PatientId INT NOT NULL,
DoctorId INT NOT NULL,
Diagnosis NVARCHAR(255),
Treatment NVARCHAR(255),
VisitDate DATE NOT NULL,
NextVisitDate DATE,
foreign key(PatientId) references Patient(PatientId),
foreign key(DoctorId) references Doctor(DoctorId)

);

create table Prescription (
PrescriptionId INT PRIMARY KEY AUTO_INCREMENT,
RecordId INT,
MedicineName NVARCHAR(100) NOT NULL,
Dosage NVARCHAR(50) NOT NULL,
Instructions NVARCHAR(255),
PrescriptionDate DATE NOT NULL,
foreign key (RecordId) references MedicalRecord(RecordId)
);

INSERT INTO Patient (FirstName, LastName, DateOfBirth, Gender, PhoneNumber, Email, Address, EmergencyContact)
VALUES
('Иван', 'Петров', '1985-03-12', 'M', '0881234567', 'ivan.petrov@email.com', 'София, ул. Витоша 23', 'Мария Петрова, 0887654321'),
('Анна', 'Георгиева', '1990-07-05', 'F', '0891122334', 'anna.georgieva@email.com', 'Пловдив, ул. Марица 45', 'Димитър Георгиев, 0898765432'),
('Петър', 'Колев', '1975-11-22', 'M', '0876543210', 'petar.kolev@email.com', 'Варна, ул. Черно море 12', 'Ирина Клева, 0871234567'),
('Мария', 'Илиева', '1988-01-15', 'F', '0881122334', 'maria.ilieva@email.com', 'Бургас, ул. Слънце 8', 'Васил Илиев, 0888765432'),
('Георги', 'Николов', '1982-09-30', 'M', '0898765432', 'georgi.nikolov@email.com', 'Русе, ул. Дунав 7', 'Елена Николова, 0891234567'),
('Диана', 'Тодорова', '1995-04-19', 'F', '0876547890', 'diana.todorova@email.com', 'Плевен, ул. Влека 10', 'Николай Тодоров, 0871239876'),
('Кирил', 'Василев', '1978-06-28', 'M', '0881237890', 'kiril.vasilev@email.com', 'Шумен, ул. Христо Ботев 30', 'Стефка Василева, 0887654321'),
('Елена', 'Костова', '1992-02-14', 'F', '0896543210', 'elena.kostova@email.com', 'Стара Загора, ул. Александровска 33', 'Борис Костов, 0891237890'),
('Владимир', 'Димитров', '1980-12-02', 'M', '0882345678', 'vladimir.dimitrov@email.com', 'Благоевград, ул. Цар Симеон 5', 'Надежда Димитрова, 0887654322'),
('Рада', 'Ангелова', '1987-08-24', 'F', '0873216549', 'rada.angelova@email.com', 'Кюстендил, ул. Бузлуджа 18', 'Иван Ангелов, 0876543210');

INSERT INTO Doctor (FirstName, LastName, Specialization, PhoneNumber, Email)
VALUES
('Иван', 'Иванов', 'Общопрактикуващ', '0888123456', 'ivan.ivanov@doctor.com'),
('Мария', 'Петрова', 'Кардиолог', '0888991122', 'maria.petrova@doctor.com'),
('Георги', 'Георгиев', 'Педиатър', '0888123400', 'georgi.georgiev@doctor.com'),
('Елена', 'Димитрова', 'Ендокринолог', '0888334455', 'elena.dimitrova@doctor.com'),
('Петър', 'Петров', 'Невролог', '0888667788', 'petar.petrov@doctor.com'),
('Анна', 'Маринова', 'Дерматолог', '0888222333', 'anna.marinova@doctor.com'),
('Николай', 'Николов', 'Офталмолог', '0888555566', 'nikolay.nikolov@doctor.com'),
('Стефан', 'Стоянов', 'Хирург', '0888447766', 'stefan.stoyanov@doctor.com'),
('Йоана', 'Йорданова', 'Гинеколог', '0888009988', 'yoana.yordanova@doctor.com'),
('Виктория', 'Костова', 'Уролог', '0888997766', 'viktoria.kostova@doctor.com');

INSERT INTO MedicalRecord (PatientId, DoctorId, Diagnosis, Treatment, VisitDate, NextVisitDate)
VALUES
(1, 1, 'Грип тип А', 'Tamiflu, почивка', '2024-01-05', '2024-01-12'),
(2, 2, 'Синузит', 'Антибиотик Аугментин', '2024-01-10', '2024-01-17'),
(3, 3, 'Ангина', 'Антибиотик Амоксициклин', '2024-01-15', '2024-01-22'),
(4, 4, 'Диабет тип 2', 'Метформин, диета', '2024-01-20', '2024-01-27'),
(5, 5, 'Мигрена', 'Ибупрофен, намаляване на стреса', '2024-01-25', '2024-02-01'),
(6, 6, 'Хипертония', 'Диуретик, диета', '2024-01-30', '2024-02-06'),
(7, 7, 'Вирусна инфекция', 'Имуностимулатори', '2024-02-04', '2024-02-11'),
(8, 8, 'Остър бронхит', 'Ингалатор, сироп за кашлица', '2024-02-09', '2024-02-16'),
(9, 9, 'Гастрит', 'Диета, протонни инхибитори', '2024-02-14', '2024-02-21'),
(10, 10, 'Анемия', 'Железни добавки', '2024-02-19', '2024-02-26');

INSERT INTO Prescription (RecordId, MedicineName, Dosage, Instructions, PrescriptionDate)
VALUES
(1, 'Tamiflu', '75mg', '2 пъти дневно', '2024-01-05'),
(2, 'Аугментин', '875mg', '1 таблетка на 12 часа', '2024-01-10'),
(3, 'Амоксициклин', '500mg', '3 пъти дневно', '2024-01-15'),
(4, 'Метформин', '850mg', '2 пъти дневно', '2024-01-20'),
(5, 'Ибупрофен', '200mg', 'при нужда от болка', '2024-01-25'),
(6, 'Диуретик', '50mg', 'сутрин', '2024-01-30'),
(7, 'Имуностимулатори', 'еднократно', 'според инструкцията', '2024-02-04'),
(8, 'Сироп за кашлица', '15ml', '3 пъти дневно', '2024-02-09'),
(9, 'Протонни инхибитори', '20mg', 'сутрин', '2024-02-14'),
(10, 'Железни добавки', '325mg', 'с храна', '2024-02-19');