INSERT INTO Adresses
VALUES 
('Sveavägen 3', 34512, 'Göteborg'),
('Gamlavägen 12', 27561, 'Göteborg'),
('Floravägen 60', 12345, 'Göteborg'),
('Ängsvägen 22', 54321, 'Göteborg'),
('Villagatan 56', 65425, 'Göteborg'),
('Nordkapsvägen 14', 25469, 'Göteborg');


INSERT INTO Roles
VALUES 
('IT Support Technician'),
('Property Caretaker'),
('Administrator');
Go

INSERT INTO Employees
VALUES 
('Lisa', 'Larsson', 2),
('Anna', 'Persson', 3),
('Bo', 'Svensson', 1),
('Theo', 'Eriksson', 2);


INSERT INTO StatusTypes
VALUES 
('Not started'),
('Ongoing'),
('Completed');
Go

INSERT INTO Clients
VALUES 
('5de5b0ca-c9a6-4ac6-a5a3-c0bdeb9adb87', 'Stina', 'Marberg', 'stina@example.com', '+4673-2545123', 3),
('5f72fd36-b497-4481-b4dd-f6a98f12fb7a', 'Emma', 'Gabrielsson', 'emma@example.com', '+4673-8502456', 2),
('e39e810a-a16d-40d9-9fe3-44e2fd9bbe98', 'Ingemar', 'Eriksson', 'ingemar@example.com', '+4673-5696811', 1),
('5be79af7-42d0-43eb-a40f-c6c0896389da', 'Siv', 'Nordholm', 'siv@example.com', '+4672-2545123', 4),
('28ad7fbe-9365-4e69-8af3-7b07b879d090', 'Sören', 'Larsson', 'sören@example.com', '+4670-5896332', 5),
('a45633ad-a9c7-4edc-9561-338bfcfe4c8d', 'Hans', 'Mattin-Lassei', 'hans@example.com', '+4670-2583699', 6);
Go

INSERT INTO Cases
VALUES 
('f98b98fb-0470-4096-90cd-0b05d8be9fc9', 'Snöras', 'Det har rasat ner jättemycket snö från taket rakt ovanför entrén, så just nu är det svårt att ta sig ut där.', '2023-03-14 14:08:54', '2023-03-14 16:00:23', 3, '28ad7fbe-9365-4e69-8af3-7b07b879d090'),
('6bad8146-7c0e-41d1-b646-377cd98d6c0e', 'Problem med fibernätet', 'Kopplingsdosan till fibernätet i lägenheten blinkar rött och fungerar inte riktigt. Provat starta om den, men den hittar ingen koppling.', '2023-03-10 15:28:14', '2023-03-13 10:08:54', 3, '5be79af7-42d0-43eb-a40f-c6c0896389da'),
('c5d7c1c5-be5c-441f-b756-3635616407af', 'Ventilationsfel', 'Det låter konstigt från ventilationen och fläkten i köket drar inte ut matoset, måste kontrolleras snarast.', '2023-03-11 10:47:02', '2023-03-13 11:15:31', 3, '5f72fd36-b497-4481-b4dd-f6a98f12fb7a');
Go

INSERT INTO Comments
VALUES 
('e8622fa6-a076-4171-80f4-e5b05bfb6470', 'Nu är det skottat utanför er entré och ärendet avslutat.', '2023-03-14 15:00:23', 'f98b98fb-0470-4096-90cd-0b05d8be9fc9', 2),
('fa324713-a369-42c0-8a93-896a1f3fad2a',  'Problemet är åtgärdat och ärendet avslutat.', '2023-03-13 10:08:54', '6bad8146-7c0e-41d1-b646-377cd98d6c0e', 3),
('192e0b5e-ed3a-4693-a535-f7496616af0b', 'Ventilationsproblemet åtgärdat och ärendet avslutat.', '2023-03-13 11:15:31', 'c5d7c1c5-be5c-441f-b756-3635616407af', 2);
Go