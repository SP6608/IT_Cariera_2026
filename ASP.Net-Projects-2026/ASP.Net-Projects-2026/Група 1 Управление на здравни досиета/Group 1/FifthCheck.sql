Use health_records;
SELECT
    m.RecordId,
    m.Diagnosis,
    m.Treatment,
    m.VisitDate,
    m.NextVisitDate
FROM
    Patient p
LEFT JOIN MedicalRecord m ON m.PatientId = p.PatientId
WHERE
    p.PatientId = 9;