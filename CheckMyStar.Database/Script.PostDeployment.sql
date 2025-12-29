IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'Country')
BEGIN
    INSERT INTO dbo.Country (Identifier, Code, Name)
    SELECT
        ROW_NUMBER() OVER (ORDER BY x.Code) AS Identifier,
        x.Code,
        x.Name
    FROM
    (
        VALUES
            ('AD','Andorre'),
            ('AE','Émirats arabes unis'),
            ('AF','Afghanistan'),
            ('AG','Antigua-et-Barbuda'),
            ('AI','Anguilla'),
            ('AL','Albanie'),
            ('AM','Arménie'),
            ('AO','Angola'),
            ('AQ','Antarctique'),
            ('AR','Argentine'),
            ('AS','Samoa américaines'),
            ('AT','Autriche'),
            ('AU','Australie'),
            ('AW','Aruba'),
            ('AX','État libre associé d''Åland'),
            ('AZ','Azerbaïdjan'),
            ('BA','Bosnie-Herzégovine'),
            ('BB','Barbade'),
            ('BD','Bangladesh'),
            ('BE','Belgique'),
            ('BF','Burkina Faso'),
            ('BG','Bulgarie'),
            ('BH','Bahreïn'),
            ('BI','Burundi'),
            ('BJ','Bénin'),
            ('BL','Saint Barthélemy'),
            ('BM','Bermuda'),
            ('BO','Bolivie'),
            ('BQ','Bonaire, Sint Eustatius and Saba'),
            ('BR','Brésil'),
            ('BS','Bahamas'),
            ('BT','Bhoutan'),
            ('BV','Île Bouvet'),
            ('BW','Botswana'),
            ('BY','Biélorussie'),
            ('BZ','Belize'),
            ('CA','Canada'),
            ('CC','Cocos (Keeling)'),
            ('CD','République démocratique du Congo'),
            ('CF','Centre Afrique'),
            ('CG','Congo'),
            ('CH','Suisse'),
            ('CI','Côte d''Ivoire'),
            ('CK','Iles Cook'),
            ('CL','Chili'),
            ('CM','Cameroun'),
            ('CN','Chine'),
            ('CO','Colombie'),
            ('CR','Costa Rica'),
            ('CU','Cuba'),
            ('CV','Subdivisions du Cap-Vert'),
            ('CW','Curaçao'),
            ('CX','Christmas Island'),
            ('CY','Chypre'),
            ('CZ','Tchécoslovaquie'),
            ('DE','Allemagne'),
            ('DJ','Djibouti'),
            ('DK','Danemark'),
            ('DM','Dominique'),
            ('DO','République dominicaine'),
            ('DZ','Algérie'),
            ('EC','Équateur'),
            ('EE','Estonie'),
            ('EG','Égypte '),
            ('EH','Sahara Occidental'),
            ('ER','Érythrée'),
            ('ES','Espagne'),
            ('ET','Éthiopie'),
            ('FI','Finlande'),
            ('FJ','Fidji'),
            ('FK','Îles Falkland (Malvinas)'),
            ('FM','Micronésie'),
            ('FO','Îles Féroé'),
            ('FR','France'),
            ('GA','Gabon'),
            ('GB','Royaume-Uni'),
            ('GD','Grenade'),
            ('GE','Géorgie'),
            ('GF','Guyane française'),
            ('GG','Guernesey'),
            ('GH','Ghana'),
            ('GI','Gibraltar'),
            ('GL','Groenland'),
            ('GM','Gambie'),
            ('GN','Guinée'),
            ('GP','Guadeloupe'),
            ('GQ','Guinée équatoriale'),
            ('GR','Grèce'),
            ('GS','Géorgie du Sud et les îles Sandwich du Sud'),
            ('GT','Guatemala'),
            ('GU','Guam'),
            ('GW','Guinée-Bissau'),
            ('GY','Guyane'),
            ('HK','Hong-Kong'),
            ('HM','Île Heard et îles McDonald'),
            ('HN','Honduras'),
            ('HR','Croatie'),
            ('HT','Haïti'),
            ('HU','Hongrie'),
            ('ID','Indonésie'),
            ('IE','Irlande'),
            ('IM','Île de Man'),
            ('IN','Inde'),
            ('IO','Territoire britannique de l''océan Indien'),
            ('IQ','Irak'),
            ('IR','Iran'),
            ('IS','Islande'),
            ('IT','Italie'),
            ('JE','Jersey'),
            ('JM','Jamaïque'),
            ('JO','Jordanie'),
            ('JP','Japon'),
            ('KE','Kenya'),
            ('KG','Kirgyzstan'),
            ('KH','Cambodge'),
            ('KI','Kiribati'),
            ('KM','Comores'),
            ('KN','Saint-Kitts-et-Nevis'),
            ('KP','Corée du nord'),
            ('KR','Corée du sud'),
            ('KW','Koweït'),
            ('KY','Îles Caïmans'),
            ('KZ','Kazakhstan'),
            ('LA','Lao'),
            ('LB','Liban'),
            ('LC','Saint Lucia'),
            ('LI','Liechtenstein'),
            ('LK','Sri Lanka'),
            ('LR','Libéria'),
            ('LS','Lesotho'),
            ('LT','Lituanie'),
            ('LU','Luxembourg'),
            ('LV','Lettonie'),
            ('LY','Libye'),
            ('MA','Maroc'),
            ('MC','Monaco'),
            ('MD','Moldavie'),
            ('ME','Monténégro'),
            ('MF','Saint Martin (partie française)'),
            ('MG','Madagascar'),
            ('MH','Îles Marshall'),
            ('MK','Macédonie'),
            ('ML','Mali'),
            ('MM','Myanmar'),
            ('MN','Mongolie'),
            ('MO','Macao'),
            ('MP','Îles Mariannes du Nord'),
            ('MQ','Martinique'),
            ('MR','Mauritanie'),
            ('MS','Montserrat'),
            ('MT','Malte'),
            ('MU','Île Maurice'),
            ('MV','Maldives'),
            ('MW','Malawi'),
            ('MX','Mexique'),
            ('MY','Malaisie'),
            ('MZ','Mozambique'),
            ('NA','Namibie'),
            ('NC','Nouvelle-Calédonie'),
            ('NE','Niger'),
            ('NF','Île Norfolk'),
            ('NG','Nigéria'),
            ('NI','Nicaragua'),
            ('NL','Pays-Bas'),
            ('NO','Norvège'),
            ('NP','Népal'),
            ('NR','Nauru'),
            ('NU','Niue'),
            ('NZ','Nouvelle-Zélande'),
            ('OM','Oman'),
            ('PA','Panama'),
            ('PE','Pérou'),
            ('PF','Polynésie française'),
            ('PG','Papouasie-Nouvelle-Guinée'),
            ('PH','Philippines'),
            ('PK','Pakistan'),
            ('PL','Pologne'),
            ('PM','Saint Pierre et Miquelon'),
            ('PN','Pitcairn'),
            ('PR','Porto Rico'),
            ('PS','Palestine'),
            ('PT','Portugal'),
            ('PW','Palau'),
            ('PY','Paraguay'),
            ('QA','Qatar'),
            ('RE','Réunion'),
            ('RO','Roumanie'),
            ('RS','Serbie'),
            ('RU','Fédération de Russie'),
            ('RW','Rwanda'),
            ('SA','Arabie Saoudite'),
            ('SB','Îles Salomon'),
            ('SC','Seychelles'),
            ('SD','Soudan'),
            ('SE','Suède'),
            ('SG','Singapour'),
            ('SH','Saint Helena, Ascension et Tristan da Cunha'),
            ('SI','Slovénie'),
            ('SJ','Svalbard et Jan Mayen'),
            ('SK','Slovaquie'),
            ('SL','Sierra Leone'),
            ('SM','Saint-Marin'),
            ('SN','Sénégal'),
            ('SO','Somalie'),
            ('SR','Suriname'),
            ('SS','Soudan du Sud'),
            ('ST','Sao Tomé-et-Principe'),
            ('SV','El Salvador'),
            ('SX','Sint Maarten (partie hollandaise)'),
            ('SY','Syrie'),
            ('SZ','Swaziland'),
            ('TC','Îles Turks et Caicos'),
            ('TD','Tchad'),
            ('TF','Territoires français du Sud'),
            ('TG','Togo'),
            ('TH','Thaïlande'),
            ('TJ','Tadzhikistan'),
            ('TK','Tokelau'),
            ('TL','Timor-Leste'),
            ('TM','Turkménistan'),
            ('TN','Tunisie'),
            ('TO','Tonga'),
            ('TR','Turquie'),
            ('TT','Trinité-et-Tobago'),
            ('TV','Tuvalu'),
            ('TW','Taïwan, Province de la Chine'),
            ('TZ','Tanzanie'),
            ('UA','Ukraine'),
            ('UG','Ouganda'),
            ('UM','United States Minor Outlying Islands'),
            ('US','États-Unis'),
            ('UY','Uruguay'),
            ('UZ','Ouzbékistan'),
            ('VA','État de Cité du Vatican'),
            ('VC','Saint-Vincent-et-les-Grenadines'),
            ('VE','Venezuela'),
            ('VG','Îles Vierges (britanniques)'),
            ('VI','Îles Vierges (États-Unis)'),
            ('VN','Viêt-Nam'),
            ('VU','Vanuatu'),
            ('WF','Wallis-et-Futuna'),
            ('WS','Samoa'),
            ('YE','Yémen'),
            ('YT','Mayotte'),
            ('ZA','Afrique du Sud'),
            ('ZM','Zambie'),
            ('ZW','Zimbabwe')
    ) AS x(Code, Name)
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.Country c
        WHERE c.Code = v.Code
    );
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'Civility')
BEGIN
    INSERT INTO dbo.Civility (Identifier, Name, Description)
    SELECT
        ROW_NUMBER() OVER (ORDER BY x.Name) AS Identifier,
        x.Name,
        x.Description
    FROM
    (
        VALUES
            ('Mr.', 'Monsieur'),
            ('Mme.', 'Madamme')
    ) AS x(Name, Description)
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.Civility c
        WHERE c.Name = v.Name
    );
END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'Role')
BEGIN
    INSERT INTO dbo.[Role] (Identifier, Name, Description)
    SELECT
        ROW_NUMBER() OVER (ORDER BY x.Name) AS Identifier,
        x.Name,
        x.Description
    FROM
    (
        VALUES
            ('Admin', 'Administrator with full access'),
            ('User', 'Regular user with limited access'),
            ('Guest', 'Guest user with minimal access')
    ) AS x(Name, Description)
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.[Role] r
        WHERE r.Name = r.Name
    );
END

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'User')
BEGIN
    DECLARE @AdminRoleIdentifier INT;
    SELECT @AdminRoleIdentifier = Identifier FROM dbo.Role WHERE Name = 'Admin';

    DECLARE @CivilityMrIdentifier INT;
    SELECT @CivilityMrIdentifier = Identifier FROM dbo.Civility WHERE Name = 'Mr.';

    INSERT INTO dbo.[User] (CivilityIdentifier, LastName, FirstName, Society, Email, Phone, PasswordHash, RoleIdentifier, AddressIdentifier)
    SELECT
        ROW_NUMBER() OVER (ORDER BY x.Name) AS Identifier,
        x.Name,
        x.Description
    FROM
    (
        VALUES
            ('Bourdon-Lopez', 'Angel', NULL, 'bourdonangel@free.fr', NULL, CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '@Admin#'), 2), @AdminRoleIdentifier, NULL),
            ('Bourdon', 'Eric', NULL, 'bourdoneric@free.fr', NULL, CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '@Eb23!Ab28?Mb14#'), 2), @AdminRoleIdentifier, NULL)
    ) AS x(Name, Description)
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.[User] u
        WHERE u.LastName = u.LastName AND u.FirstName = u.FirstName AND u.Email = u.Email
    );
END