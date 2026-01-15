Use Health_records;
SELECT
    pr.PrescriptionId,
    pr.MedicineName,
    pr.Dosage,
    pr.Instructions,
    pr.PrescriptionDate
FROM
    Prescription pr
JOIN MedicalRecord m ON pr.RecordId = m.RecordId
WHERE
    m.PatientId = 2;