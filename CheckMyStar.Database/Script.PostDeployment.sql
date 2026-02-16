IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'Country')
BEGIN
    INSERT INTO dbo.Country (Identifier, Code, [Name])
    SELECT
        ROW_NUMBER() OVER (ORDER BY x.Code) AS Identifier,
        x.Code,
        x.[Name]
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
    ) AS x(Code, [Name])
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.Country c
        WHERE c.Code = x.Code
    );
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'Civility')
BEGIN
    INSERT INTO dbo.Civility ([Identifier], [Name], [Description])
    SELECT
        x.[Identifier],
        x.[Name],
        x.[Description]
    FROM
    (
        VALUES
            (1, 'Mr.', 'Monsieur'),
            (2, 'Mme.', 'Madamme')
    ) AS x([Identifier], [Name], [Description])
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.Civility c
        WHERE c.[Identifier] = x.[Identifier]
    );
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'Role')
BEGIN
    INSERT INTO dbo.[Role] ([Identifier], [Name], [Description], [IsActive], [CreatedDate], [UpdatedDate])
    SELECT
        x.[Identifier],
        x.[Name],
        x.[Description],
        x.[IsActive],
        x.[CreatedDate],
        x.[UpdatedDate]
    FROM
    (
        VALUES
            (1, 'Administrateur', 'Accès à l''administration du back office', 1, GETDATE(), GETDATE()),
            (2, 'Inspecteur', 'Accès aux fonctionnalités inspecteur du front office', 1, GETDATE(), GETDATE()),
            (3, 'Utilisateur', 'ccès aux fonctionnalités utilisateur du front office', 1, GETDATE(), GETDATE())
    ) AS x([Identifier], [Name], [Description], [IsActive], [CreatedDate], [UpdatedDate])
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.[Role] r
        WHERE r.[Identifier] = x.[Identifier]
    );
END
GO

IF  EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'User')
BEGIN
    DECLARE @AdminRoleIdentifier INT;
    SELECT @AdminRoleIdentifier = Identifier FROM dbo.Role WHERE [Name] = 'Administrateur';

    DECLARE @CivilityMrIdentifier INT;
    SELECT @CivilityMrIdentifier = Identifier FROM dbo.Civility WHERE [Name] = 'Mr.';

    INSERT INTO dbo.[User] (
        [Identifier],
        [CivilityIdentifier],
        [LastName],
        [FirstName],
        [SocietyIdentifier],
        [Email],
        [Phone],
        [Password],
        [RoleIdentifier],
        [AddressIdentifier],
        [CreatedDate],
        [UpdatedDate]
    )
    SELECT
        x.[Identifier],
        x.[CivilityIdentifier],
        x.[LastName],
        x.[FirstName],
        NULL,
        x.[Email],
        x.[Phone],
        x.[Password],
        x.[RoleIdentifier],
        x.[AddressIdentifier],
        x.[CreatedDate],
        x.[UpdatedDate]
    FROM
    (
        VALUES
            (1, @CivilityMrIdentifier, 'Bourdon-Lopez', 'Angel', 'bourdonangel@free.fr', NULL, CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '@Admin#'), 2), @AdminRoleIdentifier, 0, GETDATE(), GETDATE()),
            (2, @CivilityMrIdentifier, 'Bourdon', 'Eric', 'bourdoneric@free.fr', NULL, CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '@Eb23!Ab28?Mb14#'), 2), @AdminRoleIdentifier, 0, GETDATE(), GETDATE())
    ) AS x(
        [Identifier],
        [CivilityIdentifier],
        [LastName],
        [FirstName],
        [Email],
        [Phone],
        [Password],
        [RoleIdentifier],
        [AddressIdentifier],
        [CreatedDate],
        [UpdatedDate]
    )
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.[User] u
        WHERE u.[Identifier] = x.[Identifier]
    );
END
GO

INSERT INTO [dbo].[CriterionType] ([TypeCode], [Label])
SELECT x.[TypeCode], x.[Label]
FROM (VALUES
    ('X',     N'Critères obligatoires'),
    ('O',     N'Critères à la carte'),
    ('NA',    N'Critères non applicables'),
    ('X_ONC', N'Critères obligatoires non compensables')
) AS x([TypeCode], [Label])
WHERE NOT EXISTS (
    SELECT 1
    FROM [dbo].[CriterionType] t
    WHERE t.[TypeCode] = x.[TypeCode]
);
GO

IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS 
               WHERE TABLE_SCHEMA = 'dbo' 
                 AND TABLE_NAME = 'StarLevel' 
                 AND COLUMN_NAME = 'LastUpdate')
BEGIN
    ALTER TABLE [dbo].[StarLevel] ADD [LastUpdate] DATETIME NULL;
END
GO

UPDATE [dbo].[StarLevel] SET LastUpdate = GETUTCDATE() WHERE LastUpdate IS NULL;
GO

INSERT INTO [dbo].[StarLevel] ([StarLevelId], [Label], [LastUpdate])
SELECT x.[StarLevelId], x.[Label], GETUTCDATE()
FROM (VALUES
    (1, N'1 étoile'),
    (2, N'2 étoiles'),
    (3, N'3 étoiles'),
    (4, N'4 étoiles'),
    (5, N'5 étoiles')
) AS x([StarLevelId], [Label])
WHERE NOT EXISTS (
    SELECT 1
    FROM [dbo].[StarLevel] s
    WHERE s.[StarLevelId] = x.[StarLevelId]
);
GO

INSERT INTO [dbo].[Criterion] ([Description], [BasePoints])
SELECT x.[Description], x.[BasePoints]
FROM (VALUES
    (N'Accueil et information du client',             CONVERT(DECIMAL(9,2), 2.00)),
    (N'Propreté des espaces communs',                 CONVERT(DECIMAL(9,2), 3.00)),
    (N'Propreté de la chambre',                       CONVERT(DECIMAL(9,2), 4.00)),
    (N'Sécurité incendie (signalétique, consignes)',  CONVERT(DECIMAL(9,2), 5.00)),
    (N'Accessibilité (informations et parcours)',     CONVERT(DECIMAL(9,2), 2.00)),
    (N'Qualité de la literie',                        CONVERT(DECIMAL(9,2), 4.00)),
    (N'Isolation phonique minimale',                  CONVERT(DECIMAL(9,2), 3.00)),
    (N'Connexion Internet (disponibilité)',           CONVERT(DECIMAL(9,2), 2.00)),
    (N'Gestion des réclamations',                     CONVERT(DECIMAL(9,2), 2.00)),
    (N'Affichage des tarifs et informations légales', CONVERT(DECIMAL(9,2), 1.00))
) AS x([Description], [BasePoints])
WHERE NOT EXISTS (
    SELECT 1
    FROM [dbo].[Criterion] c
    WHERE c.[Description] = x.[Description]
);
GO

;WITH Crit10 AS
(
    SELECT
        c.[CriterionId],
        c.[Description],
        ROW_NUMBER() OVER (ORDER BY c.[CriterionId]) AS rn
    FROM [dbo].[Criterion] c
    WHERE c.[Description] IN
    (
        N'Accueil et information du client',
        N'Propreté des espaces communs',
        N'Propreté de la chambre',
        N'Sécurité incendie (signalétique, consignes)',
        N'Accessibilité (informations et parcours)',
        N'Qualité de la literie',
        N'Isolation phonique minimale',
        N'Connexion Internet (disponibilité)',
        N'Gestion des réclamations',
        N'Affichage des tarifs et informations légales'
    )
),
MapType AS
(
    SELECT
        s.[StarLevelId],
        c10.[CriterionId],
        [TypeCode] =
            CASE
                WHEN s.[StarLevelId] = 1 THEN CASE WHEN c10.rn <= 6 THEN 'X'     WHEN c10.rn <= 8 THEN 'O'     ELSE 'NA'    END
                WHEN s.[StarLevelId] = 2 THEN CASE WHEN c10.rn <= 7 THEN 'X'     WHEN c10.rn <= 9 THEN 'O'     ELSE 'NA'    END
                WHEN s.[StarLevelId] = 3 THEN CASE WHEN c10.rn <= 7 THEN 'X'     WHEN c10.rn = 8  THEN 'X_ONC' ELSE 'O'     END
                WHEN s.[StarLevelId] = 4 THEN CASE WHEN c10.rn <= 6 THEN 'X_ONC' WHEN c10.rn <= 9 THEN 'X'     ELSE 'O'     END
                WHEN s.[StarLevelId] = 5 THEN CASE WHEN c10.rn <= 9 THEN 'X_ONC' ELSE 'X'                               END
            END
    FROM [dbo].[StarLevel] s
    CROSS JOIN Crit10 c10
)
INSERT INTO [dbo].[StarLevelCriterion] ([StarLevelId], [CriterionId], [TypeCode])
SELECT m.[StarLevelId], m.[CriterionId], m.[TypeCode]
FROM MapType m
WHERE NOT EXISTS (
    SELECT 1
    FROM [dbo].[StarLevelCriterion] slc
    WHERE slc.[StarLevelId] = m.[StarLevelId]
      AND slc.[CriterionId] = m.[CriterionId]
);
GO