Use health_records;
SELECT
    PatientId,
    FirstName,
    LastName,
    DateOfBirth,
    PhoneNumber,
    Email
FROM
    Patient
WHERE
    PatientId = 2 OR (FirstName LIKE '%Георги%' AND LastName LIKE '%Николов%');