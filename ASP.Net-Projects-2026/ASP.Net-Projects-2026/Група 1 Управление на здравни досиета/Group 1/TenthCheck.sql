Use health_records;
SELECT
    p.FirstName AS PatientFirstName,
    p.LastName AS PatientLastName,
    pr.MedicineName,
    pr.Dosage,
    pr.Instructions,
    pr.PrescriptionDate
FROM
    Patient p
LEFT JOIN
    MedicalRecord m ON p.PatientId = m.PatientId
LEFT JOIN
    Prescription pr ON m.RecordId = pr.RecordId;