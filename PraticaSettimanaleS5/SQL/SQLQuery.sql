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
