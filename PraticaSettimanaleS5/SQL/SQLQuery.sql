CREATE TABLE Clienti (
    ClienteId INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(100) NOT NULL,
    ÈAzienda BIT NOT NULL,
    CodiceFiscale NVARCHAR(16) NULL,
    PartitaIVA NVARCHAR(11) NULL,
    Indirizzo NVARCHAR(200) NOT NULL,
    Città NVARCHAR(100) NOT NULL,
    NumeroTelefono NVARCHAR(15) NOT NULL,
    Email NVARCHAR(100) NOT NULL
);

CREATE TABLE Spedizioni (
    SpedizioneId INT PRIMARY KEY IDENTITY(1,1),
    ClienteId INT NOT NULL,
    NumeroTracking NVARCHAR(50) NOT NULL,
    DataSpedizione DATETIME NOT NULL,
    Peso DECIMAL(10, 2) NOT NULL,
    CittàDestinazione NVARCHAR(100) NOT NULL,
    IndirizzoDestinazione NVARCHAR(200) NOT NULL,
    NomeDestinatario NVARCHAR(100) NOT NULL,
    CostoSpedizione DECIMAL(10, 2) NOT NULL,
    DataConsegnaPrevista DATETIME NOT NULL,
    FOREIGN KEY (ClienteId) REFERENCES Clienti(ClienteId)
);

CREATE TABLE StatiSpedizioni (
    StatoId INT PRIMARY KEY IDENTITY(1,1),
    SpedizioneId INT NOT NULL,
    Stato NVARCHAR(50) NOT NULL,
    Luogo NVARCHAR(100) NOT NULL,
    Descrizione NVARCHAR(200) NULL,
    DataAggiornamento DATETIME NOT NULL,
    FOREIGN KEY (SpedizioneId) REFERENCES Spedizioni(SpedizioneId)
);


INSERT INTO Clienti (Nome, ÈAzienda, CodiceFiscale, PartitaIVA, Indirizzo, Città, NumeroTelefono, Email)
VALUES 
('Mario Rossi', 0, 'MRORSS80A01H501U', NULL, 'Via Roma, 1', 'Roma', '0123456789', 'mario.rossi@example.com'),
('ACME S.p.A.', 1, NULL, '12345678901', 'Via Milano, 5', 'Milano', '0987654321', 'info@acme.com');


INSERT INTO Spedizioni (ClienteId, NumeroTracking, DataSpedizione, Peso, CittàDestinazione, IndirizzoDestinazione, NomeDestinatario, CostoSpedizione, DataConsegnaPrevista)
VALUES 
(1, 'TRACK123', '2023-07-10T00:00:00', 2.5, 'Napoli', 'Via Napoli, 10', 'Luigi Verdi', 15.75, '2023-07-12T00:00:00'),
(2, 'TRACK456', '2023-07-11T00:00:00', 5.0, 'Torino', 'Corso Torino, 20', 'Giovanni Bianchi', 25.50, '2023-07-14T00:00:00');


SELECT SpedizioneId, NumeroTracking FROM Spedizioni;

INSERT INTO StatiSpedizioni (SpedizioneId, Stato, Luogo, Descrizione, DataAggiornamento)
VALUES 
(3, 'In transito', 'Roma', 'Partito dal centro di smistamento', '2023-07-10T10:00:00'),
(3, 'In consegna', 'Napoli', 'In consegna al destinatario', '2023-07-12T08:00:00'),
(4, 'In transito', 'Milano', 'Partito dal centro di smistamento', '2023-07-11T11:00:00'),
(4, 'In consegna', 'Torino', 'In consegna al destinatario', '2023-07-14T09:00:00');


SELECT * FROM SPEDIZIONI;

SELECT * FROM CLIENTI;