Use health_records;
SELECT
    p.PatientId,
    CONCAT(COALESCE(p.FirstName, ''), ' ', COALESCE(p.LastName, '')) AS FullName,
    COUNT(m.RecordId) AS VisitCount
FROM
    Patient p
LEFT JOIN MedicalRecord m ON p.PatientId = m.PatientId
    AND m.VisitDate BETWEEN '2024-01-01' AND '2024-12-31'
GROUP BY
    p.PatientId
ORDER BY
    VisitCount DESC, p.PatientId ASC
LIMIT 10;