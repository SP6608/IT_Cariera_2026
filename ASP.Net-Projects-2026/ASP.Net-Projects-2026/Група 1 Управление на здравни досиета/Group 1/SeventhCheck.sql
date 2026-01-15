Use health_records;
SELECT
    p.PatientId,
    CONCAT(COALESCE(p.FirstName, ''), ' ', COALESCE(p.LastName, '')) AS FullName,
    m.NextVisitDate
FROM
    Patient p
JOIN MedicalRecord m ON p.PatientId = m.PatientId
WHERE
    m.NextVisitDate IS NOT NULL
    AND p.PatientId = 2;