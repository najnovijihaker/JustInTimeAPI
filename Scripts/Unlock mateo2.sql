-- UNLOCK TEST ACCOUNTS
UPDATE JustInTime.dbo.Accounts
SET IsLocked = 0
WHERE Username = 'mateo2';

UPDATE JustInTime.dbo.Accounts
SET IsLocked = 0
WHERE Username = 'mmajic';

UPDATE JustInTime.dbo.Accounts
SET IsLocked = 0
WHERE Username = 'test';


