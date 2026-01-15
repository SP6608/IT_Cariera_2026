Use health_records;
SELECT 
    p.PatientId,
    CONCAT(p.FirstName, ' ', p.LastName) AS FullName,
    m.Diagnosis,
    m.Treatment,
    m.VisitDate,
    pr.MedicineName,
    pr.Dosage,
    pr.Instructions
FROM 
    Patient p
LEFT JOIN MedicalRecord m ON p.PatientId = m.PatientId
LEFT JOIN Prescription pr ON m.RecordId = pr.RecordId
WHERE 
    p.PatientId = 4;