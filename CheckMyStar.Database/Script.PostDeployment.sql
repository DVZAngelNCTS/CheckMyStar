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

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'Address')
BEGIN
    DECLARE @CountryIdentifier INT;
    SELECT @CountryIdentifier = Identifier FROM dbo.Country WHERE Code = 'FR';

    INSERT INTO dbo.[Address] ([Identifier], [Number], [AddressLine], [City], [ZipCode], [Region], [CountryIdentifier], [CreatedDate], [UpdatedDate])
    SELECT
        x.[Identifier],
        x.[Number],
        x.[AddressLine],
        x.[City],
        x.[ZipCode],
        x.[Region],
        x.[CountryIdentifier],
        x.[CreatedDate],
        x.[UpdatedDate]
    FROM
    (
        VALUES
            (1, '', '', '', '', '', @CountryIdentifier, GETDATE(), GETDATE()),
            (2, '', '', '', '', '', @CountryIdentifier, GETDATE(), GETDATE())
    ) AS x([Identifier], [Number], [AddressLine], [City], [ZipCode], [Region], [CountryIdentifier], [CreatedDate], [UpdatedDate])
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.[Address] r
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

    INSERT INTO dbo.[User] ([Identifier], [CivilityIdentifier], [LastName], [FirstName], [SocietyIdentifier], [Email], [Phone], [Password], [RoleIdentifier], [AddressIdentifier], [IsActive], [IsFirstConnection], [CreatedDate], [UpdatedDate])
    SELECT
        x.[Identifier],
        x.[CivilityIdentifier],
        x.[LastName],
        x.[FirstName],
        x.[SocietyIdentifier],
        x.[Email],
        x.[Phone],
        x.[Password],
        x.[RoleIdentifier],
        x.[AddressIdentifier],
        x.[IsActive],
        x.[IsFirstConnection],
        x.[CreatedDate],
        x.[UpdatedDate]
    FROM
    (
        VALUES
            (1, @CivilityMrIdentifier, 'Bourdon-Lopez', 'Angel', NULL, 'bourdonangel@free.fr', NULL, CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '@Admin#'), 2), @AdminRoleIdentifier, 1, 1, 0, GETDATE(), GETDATE()),
            (2, @CivilityMrIdentifier, 'Bourdon', 'Eric', NULL, 'bourdoneric@free.fr', NULL, CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', '@Eb23!Ab28?Mb14#'), 2), @AdminRoleIdentifier, 2, 1, 0, GETDATE(), GETDATE())
    ) AS x([Identifier], [CivilityIdentifier], [LastName], [FirstName], [SocietyIdentifier], [Email], [Phone], [Password], [RoleIdentifier], [AddressIdentifier], [IsActive], [IsFirstConnection], [CreatedDate], [UpdatedDate])
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

INSERT INTO [dbo].[StarLevel] ([StarLevelId], [Label])
SELECT x.[StarLevelId], x.[Label]
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

-- ============================================================
-- Insertion des critères (Criterion)
-- ============================================================
INSERT INTO dbo.Criterion ([Description], [BasePoints])
SELECT x.[Description], x.[BasePoints]
FROM (
    VALUES
    (N'Surface totale minimum (cuisine et coin cuisine compris) du logement meublé hors salle d''eau et toilettes', 5),
    (N'Surface totale majorée', 1),
    (N'Prise de courant libre dans chaque pièce d''habitation', 1),
    (N'Tous les éclairages du logement fonctionnent et sont en bon état', 3),
    (N'Mise à disposition d''un téléphone privatif à l''intérieur du logement', 1),
    (N'Accès internet par un réseau local sans fil (WiFi) ', 2),
    (N'Accès internet filaire avec câble fourni ', 2),
    (N'Télévision à écran plat avec télécommande', 2),
    (N'Accès à des chaînes supplémentaires à l''offre de la TNT', 2),
    (N'Possibilité d''accéder à au moins deux chaînes internationales', 1),
    (N'Radio', 2),
    (N'Enceinte connectée', 1),
    (N'Mise à disposition d''un système de lecture de vidéos', 2),
    (N'Occultation opaque : extérieure ou intérieure dans chaque pièce comportant un couchage principal', 3),
    (N'Le logement est équipé de double vitrage', 3),
    (N'Existence d''un système de chauffage en état de fonctionnement', 5),
    (N'Existence d''un système de climatisation et / ou de rafraîchissement d''air en état de fonctionnement', 3),
    (N'Machine à laver le linge pour les logements de 4 personnes et plus', 3),
    (N'Sèche-linge électrique pour les logements de 6 personnes et plus', 2),
    (N'Etendoir ou séchoir à linge à l''intérieur du logement', 2),
    (N'Ustensiles de ménage appropriés au logement (minimum : un seau et un balai à brosse avec serpillière ou un balai de lavage à frange avec seau et presse, aspirateur ou équipement équivalent)', 3),
    (N'Fer et table à repasser', 2),
    (N'Placards ou éléments de rangement dans le logement', 3),
    (N'Placards ou éléments de rangement dans chaque pièce d''habitation', 3),
    (N'Présence d''une table et d''assises correspondant à la capacité d''accueil du logement', 4),
    (N'Présence d''un canapé ou fauteuil(s) adapté(s) à la capacité d''accueil', 3),
    (N'Présence d''une table basse', 1),
    (N'Respect des dimensions du (ou des) lit(s)', 4),
    (N'Matelas haute densité et / ou avec une épaisseur de qualité', 2),
    (N'Présence d''oreiller(s) en quantité suffisante', 2),
    (N'Deux couvertures ou une couette par lit - couette obligatoire pour les catégories 3*, 4* et 5*', 2),
    (N'Matelas et oreillers protégés par des alaises ou des housses amovibles', 2),
    (N'Eclairage en-tête de lit par personne avec interrupteur individuel', 2),
    (N'Interrupteur ou système de commande de l''éclairage central près du lit', 2),
    (N'Présence d''une prise de courant libre située près du lit', 1),
    (N'Présence d''une table de chevet par personne', 2),
    (N'Une salle d''eau privative dans un espace clos et aéré intérieur au logement', 2),
    (N'Une salle d''eau privative avec accès indépendant dans un espace intérieur au logement', 3),
    (N'Présence d''une salle d''eau ainsi équipée : un lavabo avec eau chaude, une douche et / ou une baignoire (équipée d''une douchette) avec pare-douche ; une baignoire et une douche', 3),
    (N'Présence d''une salle d''eau ainsi équipée : un lavabo avec eau chaude, une douche (dimensions supérieures au standard) et / ou une baignoire (équipée d''une douchette) avec pare-douche (dimensions supérieures au standard) ; une baignoire et une douche', 2),
    (N'Un WC (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement', 2),
    (N'Un WC (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement indépendant de la salle d''eau', 2),
    (N'Une deuxième salle d''eau privative dans un espace clos et aéré intérieur au logement avec accès indépendant', 5),
    (N'Présence d''une salle d''eau supplémentaire ainsi équipée : un lavabo avec eau chaude, une douche et / ou une baignoire (équipée d''une douchette) avec pare-douche ; une baignoire et une douche', 3),
    (N'Un WC supplémentaire (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement', 2),
    (N'Deux points lumineux dont un sur le lavabo', 2),
    (N'Présence de produits d''accueil', 3),
    (N'Une prise de courant libre à proximité du miroir', 2),
    (N'Patère(s) ou porte-serviettes', 1),
    (N'Sèche-serviettes électrique', 2),
    (N'Miroir de salle de bain', 2),
    (N'Miroir en pied', 2),
    (N'Tablette sous miroir, plan vasque ou étagère proche du miroir', 2),
    (N'Espace(s) de rangement supplémentaire(s)', 2),
    (N'Sèche-cheveux électrique en nombre suffisant', 1),
    (N'Evier avec robinet mélangeur ou mitigeur', 3),
    (N'Nombre de foyers respectés', 3),
    (N'Plaque vitrocéramique, à induction ou à gaz', 2),
    (N'Pour un mini-four', 3),
    (N'Four à micro-ondes', 2),
    (N'Ventilation ou ventilation mécanique contrôlée', 4),
    (N'Hotte aspirante', 2),
    (N'Quantité de vaisselle de table non dépareillée minimum par personne : 2 verres à eau, 1 verre à vin, 2 assiettes plates, 2 assiettes creuses, 2 assiettes à dessert, 2 grandes cuillères, 2 petites cuillères, 2 couteaux, 2 fourchettes, 2 bols, 2 tasses ou mugs', 3),
    (N'Vaisselle supplémentaire : 1 coupe à champagne, 1 verre à apéritif par personne', 1),
    (N'Équipement minimum pour la préparation des repas : 1 saladier, 1 plat allant au four, 2 casseroles, 1 poêle, 1 fait-tout, 1 tire-bouchon, 1 décapesculer, 1 paire de ciseaux, 1 planche à découper, 1 couteau à pain, 1 passoire, 1 couvercle, 1 essoreuse à salade, 1 moule à tarte et/ou moule à gâteau, 1 ouvre-boîte, 1 économe, 1 dessous de plat, 1 verre doseur, 1 louche, 1 écumoir, 1 spatule, 1 fouet', 3),
    (N'Au moins deux équipements de petit-électroménager', 2),
    (N'Autocuiseur ou cuir-vapeur ou robot de cuisine multifonctions', 3),
    (N'Cafetière', 2),
    (N'Machine à expresso', 2),
    (N'Bouilloire', 1),
    (N'Grille-pain', 1),
    (N'Lave-vaisselle pour les logements à partir de 2 personnes', 2),
    (N'Lave-vaisselle de 6 couverts ou plus pour les logements à partir de 4 personnes', 2),
    (N'Réfrigérateur avec compartiment conservateur', 4),
    (N'Présence d''un congélateur ou compartiment congélateur', 2),
    (N'Poubelle fermée avec couvercle', 1),
    (N'Pour accéder au 4ème étage à partir du rez-de-chaussée', 4),
    (N'Pour accéder au 3ème étage à partir du rez-de-chaussée', 4),
    (N'Emplacement(s) à proximité', 4),
    (N'Emplacement(s) privatif(s)', 3),
    (N'Garage ou abri couvert privatif', 2),
    (N'Logement avec balcon, loggia ou veranda (3m² minimum)', 2),
    (N'Logement avec terrasse ou jardin privé (8m² minimum)', 3),
    (N'Logement avec parc ou jardin (50m² minimum)', 2),
    (N'Présence de mobilier de jardin privatif propre et en bon état', 2),
    (N'Mise à disposition d''une planche extérieure et/ou d''un barbecue extérieur', 2),
    (N'Un équipement léger de loisirs, détente ou sport, dédié au logement', 2),
    (N'Un équipement aménagé de loisirs, détente ou sport, dédié au logement', 2),
    (N'Piscine extérieure ou intérieure', 2),
    (N'Piscine extérieure ou intérieure chauffée', 2),
    (N'Existence de rangement(s) pour équipement sportif', 1),
    (N'Logement avec vue paysagère (vue mer, montagne, plaine ou zone urbaine)', 2),
    (N'Logement avec accès immédiat à un environnement offrant la possibilité de faire des activités : nature, culture et sport', 3),
    (N'Logement avec accès immédiat aux commerces, services et transports en commun', 3),
    (N'Les sanitaires (toilette(s) et salle(s) d''eau) sont propres et en bon état', 5),
    (N'Les sols murs et plafonds sont propres et en bon état', 5),
    (N'Le mobilier est propre et en bon état', 5),
    (N'La literie est propre et en bon état', 5),
    (N'La cuisine ou coin cuisine et les équipements sont propres et en bon état', 5),
    (N'Mise à disposition de brochures d''informations locales et touristiques en français et dans au moins une langue étrangère', 3),
    (N'Mise à disposition d''un livret d''accueil', 2),
    (N'Accueil sur place par le propriétaire ou son représentant', 3),
    (N'Cadeau de bienvenue à l''arrivée du client', 2),
    (N'Existence d''une boite à clé ou système équivalent', 2),
    (N'Draps de lit proposés systématiquement par le loueur', 2),
    (N'Linge de toilette proposé systématiquement par le loueur', 2),
    (N'Linge de table', 2),
    (N'Lits faits à l''arrivée proposés systématiquement par le loueur', 2),
    (N'Matériel pour bébé à la demande', 2),
    (N'Service de ménage proposé systématiquement', 2),
    (N'Présence de produits d''entretien', 2),
    (N'Adaptateurs électriques', 2),
    (N'Existence d''un site internet ou d''une page internet dédiée au logement', 2),
    (N'Existence d''un site internet ou d''une page internet dédiée au logement en une langue étrangère', 1),
    (N'Animaux de compagnie admis', 2),
    (N'Informations concernant l''accessibilité sur les supports d''information', 2),
    (N'Mise à disposition de télécommande de télévision à grosses touches et de couleurs contrastées', 2),
    (N'Présence d''un siège de douche avec barre d''appui', 2),
    (N'Présence de WC avec barre d''appui', 2),
    (N'Largeur de toutes les portes adaptées', 2),
    (N'Document accessible mis à disposition', 1),
    (N'Obtention du label Tourisme et Handicap', 3),
    (N'Mise en œuvre d''une mesure de réduction de consommation d''énergie', 3),
    (N'Mise en œuvre d''une mesure de réduction de consommation d''énergie supplémentaire', 1),
    (N'Borne de recharge pour les véhicules électriques', 2),
    (N'Mise en œuvre d''une mesure de réduction de consommation d''eau', 3),
    (N'Mise en œuvre d''une mesure de réduction de consommation d''eau supplémentaire', 1),
    (N'Existence d''un système de tri des déchets dédié au logement', 1),
    (N'Existence d''un composteur', 1),
    (N'Sensibilisation des clients sur les actions qu''ils peuvent réaliser lors de leur séjour en matière de respect de l''environnement', 2),
    (N'Présence de produits d''accueil écologiques dans la salle de bains', 2),
    (N'Mise à disposition d''au moins deux produits d''entretien respectueux de l''environnement', 1),
    (N'Obtention d''un label environnemental', 3)
) AS x([Description], [BasePoints])
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.Criterion c WHERE c.[Description] = x.[Description]
);
GO
-- ============================================================
-- Insertion des associations (StarLevelCriterion)
-- ============================================================
;WITH CritStat AS (
    SELECT 
        x.Libelle,
        x.StarLevel,
        x.StatusCode
    FROM (
        VALUES
        (N'Surface totale minimum (cuisine et coin cuisine compris) du logement meublé hors salle d''eau et toilettes', 1, 'X'),
        (N'Surface totale minimum (cuisine et coin cuisine compris) du logement meublé hors salle d''eau et toilettes', 2, 'X'),
        (N'Surface totale minimum (cuisine et coin cuisine compris) du logement meublé hors salle d''eau et toilettes', 3, 'X'),
        (N'Surface totale minimum (cuisine et coin cuisine compris) du logement meublé hors salle d''eau et toilettes', 4, 'X'),
        (N'Surface totale minimum (cuisine et coin cuisine compris) du logement meublé hors salle d''eau et toilettes', 5, 'X'),

        (N'Surface totale majorée', 1, 'O'),
        (N'Surface totale majorée', 2, 'O'),
        (N'Surface totale majorée', 3, 'O'),
        (N'Surface totale majorée', 4, 'O'),
        (N'Surface totale majorée', 5, 'O'),

        (N'Prise de courant libre dans chaque pièce d''habitation', 1, 'X'),
        (N'Prise de courant libre dans chaque pièce d''habitation', 2, 'X'),
        (N'Prise de courant libre dans chaque pièce d''habitation', 3, 'X'),
        (N'Prise de courant libre dans chaque pièce d''habitation', 4, 'X'),
        (N'Prise de courant libre dans chaque pièce d''habitation', 5, 'X'),

        (N'Tous les éclairages du logement fonctionnent et sont en bon état', 1, 'X'),
        (N'Tous les éclairages du logement fonctionnent et sont en bon état', 2, 'X'),
        (N'Tous les éclairages du logement fonctionnent et sont en bon état', 3, 'X'),
        (N'Tous les éclairages du logement fonctionnent et sont en bon état', 4, 'X'),
        (N'Tous les éclairages du logement fonctionnent et sont en bon état', 5, 'X'),

        (N'Mise à disposition d''un téléphone privatif à l''intérieur du logement', 1, 'O'),
        (N'Mise à disposition d''un téléphone privatif à l''intérieur du logement', 2, 'O'),
        (N'Mise à disposition d''un téléphone privatif à l''intérieur du logement', 3, 'O'),
        (N'Mise à disposition d''un téléphone privatif à l''intérieur du logement', 4, 'O'),
        (N'Mise à disposition d''un téléphone privatif à l''intérieur du logement', 5, 'O'),

        (N'Accès internet par un réseau local sans fil (WiFi) ', 1, 'O'),
        (N'Accès internet par un réseau local sans fil (WiFi) ', 2, 'X'),
        (N'Accès internet par un réseau local sans fil (WiFi) ', 3, 'X'),
        (N'Accès internet par un réseau local sans fil (WiFi) ', 4, 'X'),
        (N'Accès internet par un réseau local sans fil (WiFi) ', 5, 'X'),

        (N'Accès internet filaire avec câble fourni ', 1, 'O'),
        (N'Accès internet filaire avec câble fourni ', 2, 'O'),
        (N'Accès internet filaire avec câble fourni ', 3, 'O'),
        (N'Accès internet filaire avec câble fourni ', 4, 'O'),
        (N'Accès internet filaire avec câble fourni ', 5, 'O'),

        (N'Télévision à écran plat avec télécommande', 1, 'O'),
        (N'Télévision à écran plat avec télécommande', 2, 'X'),
        (N'Télévision à écran plat avec télécommande', 3, 'X'),
        (N'Télévision à écran plat avec télécommande', 4, 'X'),
        (N'Télévision à écran plat avec télécommande', 5, 'X'),

        (N'Accès à des chaînes supplémentaires à l''offre de la TNT', 1, 'O'),
        (N'Accès à des chaînes supplémentaires à l''offre de la TNT', 2, 'O'),
        (N'Accès à des chaînes supplémentaires à l''offre de la TNT', 3, 'O'),
        (N'Accès à des chaînes supplémentaires à l''offre de la TNT', 4, 'X'),
        (N'Accès à des chaînes supplémentaires à l''offre de la TNT', 5, 'X'),

        (N'Possibilité d''accéder à au moins deux chaînes internationales', 1, 'O'),
        (N'Possibilité d''accéder à au moins deux chaînes internationales ', 2, 'O'),
        (N'Possibilité d''accéder à au moins deux chaînes internationales ', 3, 'O'),
        (N'Possibilité d''accéder à au moins deux chaînes internationales ', 4, 'O'),
        (N'Possibilité d''accéder à au moins deux chaînes internationales ', 5, 'X'),

	    (N'Radio', 1, 'O'),
        (N'Radio', 2, 'O'),
        (N'Radio', 3, 'X'),
        (N'Radio', 4, 'X'),
        (N'Radio', 5, 'X'),

        (N'Enceinte connectée', 1, 'O'),
        (N'Enceinte connectée', 2, 'O'),
        (N'Enceinte connectée', 3, 'O'),
        (N'Enceinte connectée', 4, 'O'),
        (N'Enceinte connectée', 5, 'X'),

        (N'Mise à disposition d''un système de lecture de vidéos', 1, 'O'),
        (N'Mise à disposition d''un système de lecture de vidéos', 2, 'O'),
        (N'Mise à disposition d''un système de lecture de vidéos', 3, 'O'),
        (N'Mise à disposition d''un système de lecture de vidéos', 4, 'O'),
        (N'Mise à disposition d''un système de lecture de vidéos', 5, 'X'),

        (N'Occultation opaque : extérieure ou intérieure dans chaque pièce comportant un couchage principal', 1, 'X'),
        (N'Occultation opaque : extérieure ou intérieure dans chaque pièce comportant un couchage principal', 2, 'X'),
        (N'Occultation opaque : extérieure ou intérieure dans chaque pièce comportant un couchage principal', 3, 'X'),
        (N'Occultation opaque : extérieure ou intérieure dans chaque pièce comportant un couchage principal', 4, 'X'),
        (N'Occultation opaque : extérieure ou intérieure dans chaque pièce comportant un couchage principal', 5, 'X'),

        (N'Le logement est équipé de double vitrage', 1, 'O'),
        (N'Le logement est équipé de double vitrage ', 2, 'O'),
        (N'Le logement est équipé de double vitrage ', 3, 'O'),
        (N'Le logement est équipé de double vitrage ', 4, 'X'),
        (N'Le logement est équipé de double vitrage ', 5, 'X'),

        (N'Existence d''un système de chauffage en état de fonctionnement', 1, 'X'),
        (N'Existence d''un système de chauffage en état de fonctionnement', 2, 'X'),
        (N'Existence d''un système de chauffage en état de fonctionnement', 3, 'X'),
        (N'Existence d''un système de chauffage en état de fonctionnement', 4, 'X'),
        (N'Existence d''un système de chauffage en état de fonctionnement', 5, 'X'),

        (N'Existence d''un système de climatisation et / ou de rafraîchissement d''air en état de fonctionnement', 1, 'O'),
        (N'Existence d''un système de climatisation et / ou de rafraîchissement d''air en état de fonctionnement', 2, 'O'),
        (N'Existence d''un système de climatisation et / ou de rafraîchissement d''air en état de fonctionnement', 3, 'O'),
        (N'Existence d''un système de climatisation et / ou de rafraîchissement d''air en état de fonctionnement', 4, 'O'),
        (N'Existence d''un système de climatisation et / ou de rafraîchissement d''air en état de fonctionnement', 5, 'X'),

        -- Critère 18
        (N'Machine à laver le linge pour les logements de 4 personnes et plus', 1, 'O'),
        (N'Machine à laver le linge pour les logements de 4 personnes et plus', 2, 'O'),
        (N'Machine à laver le linge pour les logements de 4 personnes et plus', 3, 'X'),
        (N'Machine à laver le linge pour les logements de 4 personnes et plus', 4, 'X'),
        (N'Machine à laver le linge pour les logements de 4 personnes et plus', 5, 'X'),
        -- Critère 19
        (N'Sèche-linge électrique pour les logements de 6 personnes et plus', 1, 'O'),
        (N'Sèche-linge électrique pour les logements de 6 personnes et plus', 2, 'O'),
        (N'Sèche-linge électrique pour les logements de 6 personnes et plus', 3, 'O'),
        (N'Sèche-linge électrique pour les logements de 6 personnes et plus', 4, 'X'),
        (N'Sèche-linge électrique pour les logements de 6 personnes et plus', 5, 'X'),
        -- Critère 20
        (N'Etendoir ou séchoir à linge à l''intérieur du logement', 1, 'X'),
        (N'Etendoir ou séchoir à linge à l''intérieur du logement', 2, 'X'),
        (N'Etendoir ou séchoir à linge à l''intérieur du logement', 3, 'X'),
        (N'Etendoir ou séchoir à linge à l''intérieur du logement', 4, 'X'),
        (N'Etendoir ou séchoir à linge à l''intérieur du logement', 5, 'X'),
        -- Critère 21
        (N'Ustensiles de ménage appropriés au logement (minimum : un seau et un balai à brosse avec serpillière ou un balai de lavage à frange avec seau et presse, aspirateur ou équipement équivalent)', 1, 'X'),
        (N'Ustensiles de ménage appropriés au logement (minimum : un seau et un balai à brosse avec serpillière ou un balai de lavage à frange avec seau et presse, aspirateur ou équipement équivalent)', 2, 'X'),
        (N'Ustensiles de ménage appropriés au logement (minimum : un seau et un balai à brosse avec serpillière ou un balai de lavage à frange avec seau et presse, aspirateur ou équipement équivalent)', 3, 'X'),
        (N'Ustensiles de ménage appropriés au logement (minimum : un seau et un balai à brosse avec serpillière ou un balai de lavage à frange avec seau et presse, aspirateur ou équipement équivalent)', 4, 'X'),
        (N'Ustensiles de ménage appropriés au logement (minimum : un seau et un balai à brosse avec serpillière ou un balai de lavage à frange avec seau et presse, aspirateur ou équipement équivalent)', 5, 'X'),
        -- Critère 22
        (N'Fer et table à repasser', 1, 'O'),
        (N'Fer et table à repasser', 2, 'O'),
        (N'Fer et table à repasser', 3, 'X'),
        (N'Fer et table à repasser', 4, 'X'),
        (N'Fer et table à repasser', 5, 'X'),
        -- Critère 23
        (N'Placards ou éléments de rangement dans le logement', 1, 'X'),
        (N'Placards ou éléments de rangement dans le logement', 2, 'X'),
        (N'Placards ou éléments de rangement dans le logement', 3, 'NA'),
        (N'Placards ou éléments de rangement dans le logement', 4, 'NA'),
        (N'Placards ou éléments de rangement dans le logement', 5, 'NA'),
        -- Critère 24
        (N'Placards ou éléments de rangement dans chaque pièce d''habitation', 1, 'O'),
        (N'Placards ou éléments de rangement dans chaque pièce d''habitation', 2, 'O'),
        (N'Placards ou éléments de rangement dans chaque pièce d''habitation', 3, 'X'),
        (N'Placards ou éléments de rangement dans chaque pièce d''habitation', 4, 'X'),
        (N'Placards ou éléments de rangement dans chaque pièce d''habitation', 5, 'X'),
        -- Critère 25
        (N'Présence d''une table et d''assises correspondant à la capacité d''accueil du logement', 1, 'X'),
        (N'Présence d''une table et d''assises correspondant à la capacité d''accueil du logement', 2, 'X'),
        (N'Présence d''une table et d''assises correspondant à la capacité d''accueil du logement', 3, 'X'),
        (N'Présence d''une table et d''assises correspondant à la capacité d''accueil du logement', 4, 'X'),
        (N'Présence d''une table et d''assises correspondant à la capacité d''accueil du logement', 5, 'X'),
        -- Critère 26
        (N'Présence d''un canapé ou fauteuil(s) adapté(s) à la capacité d''accueil', 1, 'X'),
        (N'Présence d''un canapé ou fauteuil(s) adapté(s) à la capacité d''accueil', 2, 'X'),
        (N'Présence d''un canapé ou fauteuil(s) adapté(s) à la capacité d''accueil', 3, 'X'),
        (N'Présence d''un canapé ou fauteuil(s) adapté(s) à la capacité d''accueil', 4, 'X'),
        (N'Présence d''un canapé ou fauteuil(s) adapté(s) à la capacité d''accueil', 5, 'X'),
        -- Critère 27
        (N'Présence d''une table basse', 1, 'X'),
        (N'Présence d''une table basse', 2, 'X'),
        (N'Présence d''une table basse', 3, 'X'),
        (N'Présence d''une table basse', 4, 'X'),
        (N'Présence d''une table basse', 5, 'X'),
        -- Critère 28
        (N'Respect des dimensions du (ou des) lit(s)', 1, 'X'),
        (N'Respect des dimensions du (ou des) lit(s)', 2, 'X'),
        (N'Respect des dimensions du (ou des) lit(s)', 3, 'X'),
        (N'Respect des dimensions du (ou des) lit(s)', 4, 'X'),
        (N'Respect des dimensions du (ou des) lit(s)', 5, 'X'),
        -- Critère 29
        (N'Matelas haute densité et / ou avec une épaisseur de qualité', 1, 'O'),
        (N'Matelas haute densité et / ou avec une épaisseur de qualité', 2, 'O'),
        (N'Matelas haute densité et / ou avec une épaisseur de qualité', 3, 'O'),
        (N'Matelas haute densité et / ou avec une épaisseur de qualité', 4, 'O'),
        (N'Matelas haute densité et / ou avec une épaisseur de qualité', 5, 'O'),
        -- Critère 30
        (N'Présence d''oreiller(s) en quantité suffisante', 1, 'X'),
        (N'Présence d''oreiller(s) en quantité suffisante', 2, 'X'),
        (N'Présence d''oreiller(s) en quantité suffisante', 3, 'X'),
        (N'Présence d''oreiller(s) en quantité suffisante', 4, 'X'),
        (N'Présence d''oreiller(s) en quantité suffisante', 5, 'X'),
        -- Critère 31
        (N'Deux couvertures ou une couette par lit - couette obligatoire pour les catégories 3*, 4* et 5*', 1, 'X'),
        (N'Deux couvertures ou une couette par lit - couette obligatoire pour les catégories 3*, 4* et 5*', 2, 'X'),
        (N'Deux couvertures ou une couette par lit - couette obligatoire pour les catégories 3*, 4* et 5*', 3, 'X'),
        (N'Deux couvertures ou une couette par lit - couette obligatoire pour les catégories 3*, 4* et 5*', 4, 'X'),
        (N'Deux couvertures ou une couette par lit - couette obligatoire pour les catégories 3*, 4* et 5*', 5, 'X'),
        -- Critère 32
        (N'Matelas et oreillers protégés par des alaises ou des housses amovibles', 1, 'X'),
        (N'Matelas et oreillers protégés par des alaises ou des housses amovibles', 2, 'X'),
        (N'Matelas et oreillers protégés par des alaises ou des housses amovibles', 3, 'X'),
        (N'Matelas et oreillers protégés par des alaises ou des housses amovibles', 4, 'X'),
        (N'Matelas et oreillers protégés par des alaises ou des housses amovibles', 5, 'X'),
        -- Critère 33
        (N'Eclairage en-tête de lit par personne avec interrupteur individuel', 1, 'X'),
        (N'Eclairage en-tête de lit par personne avec interrupteur individuel', 2, 'X'),
        (N'Eclairage en-tête de lit par personne avec interrupteur individuel', 3, 'X'),
        (N'Eclairage en-tête de lit par personne avec interrupteur individuel', 4, 'X'),
        (N'Eclairage en-tête de lit par personne avec interrupteur individuel', 5, 'X'),
        -- Critère 34
        (N'Interrupteur ou système de commande de l''éclairage central près du lit', 1, 'O'),
        (N'Interrupteur ou système de commande de l''éclairage central près du lit', 2, 'O'),
        (N'Interrupteur ou système de commande de l''éclairage central près du lit', 3, 'O'),
        (N'Interrupteur ou système de commande de l''éclairage central près du lit', 4, 'X'),
        (N'Interrupteur ou système de commande de l''éclairage central près du lit', 5, 'X'),
        -- Critère 35
        (N'Présence d''une prise de courant libre située près du lit', 1, 'O'),
        (N'Présence d''une prise de courant libre située près du lit', 2, 'O'),
        (N'Présence d''une prise de courant libre située près du lit', 3, 'O'),
        (N'Présence d''une prise de courant libre située près du lit', 4, 'O'),
        (N'Présence d''une prise de courant libre située près du lit', 5, 'O'),
        -- Critère 36
        (N'Présence d''une table de chevet par personne', 1, 'O'),
        (N'Présence d''une table de chevet par personne', 2, 'O'),
        (N'Présence d''une table de chevet par personne', 3, 'X'),
        (N'Présence d''une table de chevet par personne', 4, 'X'),
        (N'Présence d''une table de chevet par personne', 5, 'X'),
        -- Critère 37
        (N'Une salle d''eau privative dans un espace clos et aéré intérieur au logement', 1, 'X'),
        (N'Une salle d''eau privative dans un espace clos et aéré intérieur au logement', 2, 'X'),
        (N'Une salle d''eau privative dans un espace clos et aéré intérieur au logement', 3, 'X'),
        (N'Une salle d''eau privative dans un espace clos et aéré intérieur au logement', 4, 'X'),
        (N'Une salle d''eau privative dans un espace clos et aéré intérieur au logement', 5, 'X'),
        -- Critère 38
        (N'Une salle d''eau privative avec accès indépendant dans un espace intérieur au logement', 1, 'X'),
        (N'Une salle d''eau privative avec accès indépendant dans un espace intérieur au logement', 2, 'X'),
        (N'Une salle d''eau privative avec accès indépendant dans un espace intérieur au logement', 3, 'X'),
        (N'Une salle d''eau privative avec accès indépendant dans un espace intérieur au logement', 4, 'X'),
        (N'Une salle d''eau privative avec accès indépendant dans un espace intérieur au logement', 5, 'X'),
        -- Critère 39
        (N'Présence d''une salle d''eau ainsi équipée : un lavabo avec eau chaude, une douche et / ou une baignoire (équipée d''une douchette) avec pare-douche ; une baignoire et une douche', 1, 'X'),
        (N'Présence d''une salle d''eau ainsi équipée : un lavabo avec eau chaude, une douche et / ou une baignoire (équipée d''une douchette) avec pare-douche ; une baignoire et une douche', 2, 'X'),
        (N'Présence d''une salle d''eau ainsi équipée : un lavabo avec eau chaude, une douche et / ou une baignoire (équipée d''une douchette) avec pare-douche ; une baignoire et une douche', 3, 'X'),
        (N'Présence d''une salle d''eau ainsi équipée : un lavabo avec eau chaude, une douche et / ou une baignoire (équipée d''une douchette) avec pare-douche ; une baignoire et une douche', 4, 'NA'),
        (N'Présence d''une salle d''eau ainsi équipée : un lavabo avec eau chaude, une douche et / ou une baignoire (équipée d''une douchette) avec pare-douche ; une baignoire et une douche', 5, 'NA'),
        -- Critère 40
        (N'Présence d''une salle d''eau ainsi équipée : un lavabo avec eau chaude, une douche (dimensions supérieures au standard) et / ou une baignoire (équipée d''une douchette) avec pare-douche (dimensions supérieures au standard) ; une baignoire et une douche', 1, 'O'),
        (N'Présence d''une salle d''eau ainsi équipée : un lavabo avec eau chaude, une douche (dimensions supérieures au standard) et / ou une baignoire (équipée d''une douchette) avec pare-douche (dimensions supérieures au standard) ; une baignoire et une douche', 2, 'O'),
        (N'Présence d''une salle d''eau ainsi équipée : un lavabo avec eau chaude, une douche (dimensions supérieures au standard) et / ou une baignoire (équipée d''une douchette) avec pare-douche (dimensions supérieures au standard) ; une baignoire et une douche', 3, 'O'),
        (N'Présence d''une salle d''eau ainsi équipée : un lavabo avec eau chaude, une douche (dimensions supérieures au standard) et / ou une baignoire (équipée d''une douchette) avec pare-douche (dimensions supérieures au standard) ; une baignoire et une douche', 4, 'X'),
        (N'Présence d''une salle d''eau ainsi équipée : un lavabo avec eau chaude, une douche (dimensions supérieures au standard) et / ou une baignoire (équipée d''une douchette) avec pare-douche (dimensions supérieures au standard) ; une baignoire et une douche', 5, 'X'),
        -- Critère 41
        (N'Un WC (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement', 1, 'X'),
        (N'Un WC (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement', 2, 'X'),
        (N'Un WC (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement', 3, 'X'),
        (N'Un WC (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement', 4, 'X'),
        (N'Un WC (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement', 5, 'NA'),
        -- Critère 42
        (N'Un WC (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement indépendant de la salle d''eau', 1, 'O'),
        (N'Un WC (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement indépendant de la salle d''eau', 2, 'O'),
        (N'Un WC (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement indépendant de la salle d''eau', 3, 'O'),
        (N'Un WC (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement indépendant de la salle d''eau', 4, 'O'),
        (N'Un WC (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement indépendant de la salle d''eau', 5, 'X'),
        -- Critère 43
        (N'Une deuxième salle d''eau privative dans un espace clos et aéré intérieur au logement avec accès indépendant', 1, 'X'),
        (N'Une deuxième salle d''eau privative dans un espace clos et aéré intérieur au logement avec accès indépendant', 2, 'X'),
        (N'Une deuxième salle d''eau privative dans un espace clos et aéré intérieur au logement avec accès indépendant', 3, 'X'),
        (N'Une deuxième salle d''eau privative dans un espace clos et aéré intérieur au logement avec accès indépendant', 4, 'X'),
        (N'Une deuxième salle d''eau privative dans un espace clos et aéré intérieur au logement avec accès indépendant', 5, 'X'),
        -- Critère 44 (salle d'eau supplémentaire)
        (N'Présence d''une salle d''eau supplémentaire ainsi équipée : un lavabo avec eau chaude, une douche et / ou une baignoire (équipée d''une douchette) avec pare-douche ; une baignoire et une douche', 1, 'X'),
        (N'Présence d''une salle d''eau supplémentaire ainsi équipée : un lavabo avec eau chaude, une douche et / ou une baignoire (équipée d''une douchette) avec pare-douche ; une baignoire et une douche', 2, 'X'),
        (N'Présence d''une salle d''eau supplémentaire ainsi équipée : un lavabo avec eau chaude, une douche et / ou une baignoire (équipée d''une douchette) avec pare-douche ; une baignoire et une douche', 3, 'X'),
        (N'Présence d''une salle d''eau supplémentaire ainsi équipée : un lavabo avec eau chaude, une douche et / ou une baignoire (équipée d''une douchette) avec pare-douche ; une baignoire et une douche', 4, 'X'),
        (N'Présence d''une salle d''eau supplémentaire ainsi équipée : un lavabo avec eau chaude, une douche et / ou une baignoire (équipée d''une douchette) avec pare-douche ; une baignoire et une douche', 5, 'X'),
        -- Critère 45 (WC supplémentaire)
        (N'Un WC supplémentaire (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement', 1, 'X'),
        (N'Un WC supplémentaire (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement', 2, 'X'),
        (N'Un WC supplémentaire (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement', 3, 'X'),
        (N'Un WC supplémentaire (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement', 4, 'X'),
        (N'Un WC supplémentaire (avec cuvette, abattant, chasse d''eau, dérouleur et poubelle) privatif intérieur au logement', 5, 'X'),
        -- Critère 46
        (N'Deux points lumineux dont un sur le lavabo', 1, 'O'),
        (N'Deux points lumineux dont un sur le lavabo', 2, 'O'),
        (N'Deux points lumineux dont un sur le lavabo', 3, 'O'),
        (N'Deux points lumineux dont un sur le lavabo', 4, 'X'),
        (N'Deux points lumineux dont un sur le lavabo', 5, 'X'),
        -- Critère 47
        (N'Présence de produits d''accueil', 1, 'O'),
        (N'Présence de produits d''accueil', 2, 'O'),
        (N'Présence de produits d''accueil', 3, 'O'),
        (N'Présence de produits d''accueil', 4, 'X'),
        (N'Présence de produits d''accueil', 5, 'X'),
        -- Critère 48
        (N'Une prise de courant libre à proximité du miroir', 1, 'O'),
        (N'Une prise de courant libre à proximité du miroir', 2, 'O'),
        (N'Une prise de courant libre à proximité du miroir', 3, 'X'),
        (N'Une prise de courant libre à proximité du miroir', 4, 'X'),
        (N'Une prise de courant libre à proximité du miroir', 5, 'X'),
        -- Critère 49
        (N'Patère(s) ou porte-serviettes', 1, 'X'),
        (N'Patère(s) ou porte-serviettes', 2, 'X'),
        (N'Patère(s) ou porte-serviettes', 3, 'X'),
        (N'Patère(s) ou porte-serviettes', 4, 'X'),
        (N'Patère(s) ou porte-serviettes', 5, 'X'),
        -- Critère 50
        (N'Sèche-serviettes électrique', 1, 'O'),
        (N'Sèche-serviettes électrique', 2, 'O'),
        (N'Sèche-serviettes électrique', 3, 'O'),
        (N'Sèche-serviettes électrique', 4, 'X'),
        (N'Sèche-serviettes électrique', 5, 'X'),
        -- Critère 51
        (N'Miroir de salle de bain', 1, 'X'),
        (N'Miroir de salle de bain', 2, 'X'),
        (N'Miroir de salle de bain', 3, 'X'),
        (N'Miroir de salle de bain', 4, 'X'),
        (N'Miroir de salle de bain', 5, 'X'),
        -- Critère 52
        (N'Miroir en pied', 1, 'O'),
        (N'Miroir en pied', 2, 'O'),
        (N'Miroir en pied', 3, 'O'),
        (N'Miroir en pied', 4, 'X'),
        (N'Miroir en pied', 5, 'X'),
        -- Critère 53
        (N'Tablette sous miroir, plan vasque ou étagère proche du miroir', 1, 'X'),
        (N'Tablette sous miroir, plan vasque ou étagère proche du miroir', 2, 'X'),
        (N'Tablette sous miroir, plan vasque ou étagère proche du miroir', 3, 'X'),
        (N'Tablette sous miroir, plan vasque ou étagère proche du miroir', 4, 'X'),
        (N'Tablette sous miroir, plan vasque ou étagère proche du miroir', 5, 'X'),
        -- Critère 54
        (N'Espace(s) de rangement supplémentaire(s)', 1, 'O'),
        (N'Espace(s) de rangement supplémentaire(s)', 2, 'O'),
        (N'Espace(s) de rangement supplémentaire(s)', 3, 'O'),
        (N'Espace(s) de rangement supplémentaire(s)', 4, 'X'),
        (N'Espace(s) de rangement supplémentaire(s)', 5, 'X'),
        -- Critère 55
        (N'Sèche-cheveux électrique en nombre suffisant', 1, 'O'),
        (N'Sèche-cheveux électrique en nombre suffisant', 2, 'O'),
        (N'Sèche-cheveux électrique en nombre suffisant', 3, 'O'),
        (N'Sèche-cheveux électrique en nombre suffisant', 4, 'X'),
        (N'Sèche-cheveux électrique en nombre suffisant', 5, 'X'),
        -- Critère 56
        (N'Evier avec robinet mélangeur ou mitigeur', 1, 'X'),
        (N'Evier avec robinet mélangeur ou mitigeur', 2, 'X'),
        (N'Evier avec robinet mélangeur ou mitigeur', 3, 'X'),
        (N'Evier avec robinet mélangeur ou mitigeur', 4, 'X'),
        (N'Evier avec robinet mélangeur ou mitigeur', 5, 'X'),
        -- Critère 57
        (N'Nombre de foyers respectés', 1, 'X'),
        (N'Nombre de foyers respectés', 2, 'X'),
        (N'Nombre de foyers respectés', 3, 'X'),
        (N'Nombre de foyers respectés', 4, 'X'),
        (N'Nombre de foyers respectés', 5, 'X'),

        (N'Plaque vitrocéramique, à induction ou à gaz', 1, 'O'),
        (N'Plaque vitrocéramique, à induction ou à gaz', 2, 'O'),
        (N'Plaque vitrocéramique, à induction ou à gaz', 3, 'O'),
        (N'Plaque vitrocéramique, à induction ou à gaz', 4, 'O'),
        (N'Plaque vitrocéramique, à induction ou à gaz', 5, 'O'),

        -- Critère 58
        (N'Pour un mini-four', 1, 'X'),
        (N'Pour un mini-four', 2, 'X'),
        (N'Pour un mini-four', 3, 'X'),
        (N'Pour un mini-four', 4, 'X'),
        (N'Pour un mini-four', 5, 'X'),
        -- Critère 59
        (N'Four à micro-ondes', 1, 'O'),
        (N'Four à micro-ondes', 2, 'X'),
        (N'Four à micro-ondes', 3, 'X'),
        (N'Four à micro-ondes', 4, 'X'),
        (N'Four à micro-ondes', 5, 'X'),
        -- Critère 60
        (N'Ventilation ou ventilation mécanique contrôlée', 1, 'X'),
        (N'Ventilation ou ventilation mécanique contrôlée', 2, 'X'),
        (N'Ventilation ou ventilation mécanique contrôlée', 3, 'X'),
        (N'Ventilation ou ventilation mécanique contrôlée', 4, 'X'),
        (N'Ventilation ou ventilation mécanique contrôlée', 5, 'X'),
        -- Critère 61
        (N'Hotte aspirante', 1, 'O'),
        (N'Hotte aspirante', 2, 'O'),
        (N'Hotte aspirante', 3, 'O'),
        (N'Hotte aspirante', 4, 'O'),
        (N'Hotte aspirante', 5, 'O'),
        -- Critère 62
        (N'Quantité de vaisselle de table non dépareillée minimum par personne : 2 verres à eau, 1 verre à vin, 2 assiettes plates, 2 assiettes creuses, 2 assiettes à dessert, 2 grandes cuillères, 2 petites cuillères, 2 couteaux, 2 fourchettes, 2 bols, 2 tasses ou mugs', 1, 'X'),
        (N'Quantité de vaisselle de table non dépareillée minimum par personne : 2 verres à eau, 1 verre à vin, 2 assiettes plates, 2 assiettes creuses, 2 assiettes à dessert, 2 grandes cuillères, 2 petites cuillères, 2 couteaux, 2 fourchettes, 2 bols, 2 tasses ou mugs', 2, 'X'),
        (N'Quantité de vaisselle de table non dépareillée minimum par personne : 2 verres à eau, 1 verre à vin, 2 assiettes plates, 2 assiettes creuses, 2 assiettes à dessert, 2 grandes cuillères, 2 petites cuillères, 2 couteaux, 2 fourchettes, 2 bols, 2 tasses ou mugs', 3, 'X'),
        (N'Quantité de vaisselle de table non dépareillée minimum par personne : 2 verres à eau, 1 verre à vin, 2 assiettes plates, 2 assiettes creuses, 2 assiettes à dessert, 2 grandes cuillères, 2 petites cuillères, 2 couteaux, 2 fourchettes, 2 bols, 2 tasses ou mugs', 4, 'X'),
        (N'Quantité de vaisselle de table non dépareillée minimum par personne : 2 verres à eau, 1 verre à vin, 2 assiettes plates, 2 assiettes creuses, 2 assiettes à dessert, 2 grandes cuillères, 2 petites cuillères, 2 couteaux, 2 fourchettes, 2 bols, 2 tasses ou mugs', 5, 'X'),
        -- Critère 63
        (N'Vaisselle supplémentaire : 1 coupe à champagne, 1 verre à apéritif par personne', 1, 'O'),
        (N'Vaisselle supplémentaire : 1 coupe à champagne, 1 verre à apéritif par personne', 2, 'O'),
        (N'Vaisselle supplémentaire : 1 coupe à champagne, 1 verre à apéritif par personne', 3, 'O'),
        (N'Vaisselle supplémentaire : 1 coupe à champagne, 1 verre à apéritif par personne', 4, 'X'),
        (N'Vaisselle supplémentaire : 1 coupe à champagne, 1 verre à apéritif par personne', 5, 'X'),
        -- Critère 64
        (N'Équipement minimum pour la préparation des repas : 1 saladier, 1 plat allant au four, 2 casseroles, 1 poêle, 1 fait-tout, 1 tire-bouchon, 1 décapesculer, 1 paire de ciseaux, 1 planche à découper, 1 couteau à pain, 1 passoire, 1 couvercle, 1 essoreuse à salade, 1 moule à tarte et/ou moule à gâteau, 1 ouvre-boîte, 1 économe, 1 dessous de plat, 1 verre doseur, 1 louche, 1 écumoir, 1 spatule, 1 fouet', 1, 'X'),
        (N'Équipement minimum pour la préparation des repas : 1 saladier, 1 plat allant au four, 2 casseroles, 1 poêle, 1 fait-tout, 1 tire-bouchon, 1 décapesculer, 1 paire de ciseaux, 1 planche à découper, 1 couteau à pain, 1 passoire, 1 couvercle, 1 essoreuse à salade, 1 moule à tarte et/ou moule à gâteau, 1 ouvre-boîte, 1 économe, 1 dessous de plat, 1 verre doseur, 1 louche, 1 écumoir, 1 spatule, 1 fouet', 2, 'X'),
        (N'Équipement minimum pour la préparation des repas : 1 saladier, 1 plat allant au four, 2 casseroles, 1 poêle, 1 fait-tout, 1 tire-bouchon, 1 décapesculer, 1 paire de ciseaux, 1 planche à découper, 1 couteau à pain, 1 passoire, 1 couvercle, 1 essoreuse à salade, 1 moule à tarte et/ou moule à gâteau, 1 ouvre-boîte, 1 économe, 1 dessous de plat, 1 verre doseur, 1 louche, 1 écumoir, 1 spatule, 1 fouet', 3, 'X'),
        (N'Équipement minimum pour la préparation des repas : 1 saladier, 1 plat allant au four, 2 casseroles, 1 poêle, 1 fait-tout, 1 tire-bouchon, 1 décapesculer, 1 paire de ciseaux, 1 planche à découper, 1 couteau à pain, 1 passoire, 1 couvercle, 1 essoreuse à salade, 1 moule à tarte et/ou moule à gâteau, 1 ouvre-boîte, 1 économe, 1 dessous de plat, 1 verre doseur, 1 louche, 1 écumoir, 1 spatule, 1 fouet', 4, 'X'),
        (N'Équipement minimum pour la préparation des repas : 1 saladier, 1 plat allant au four, 2 casseroles, 1 poêle, 1 fait-tout, 1 tire-bouchon, 1 décapesculer, 1 paire de ciseaux, 1 planche à découper, 1 couteau à pain, 1 passoire, 1 couvercle, 1 essoreuse à salade, 1 moule à tarte et/ou moule à gâteau, 1 ouvre-boîte, 1 économe, 1 dessous de plat, 1 verre doseur, 1 louche, 1 écumoir, 1 spatule, 1 fouet', 5, 'X'),
        -- Critère 65
        (N'Au moins deux équipements de petit-électroménager', 1, 'O'),
        (N'Au moins deux équipements de petit-électroménager', 2, 'O'),
        (N'Au moins deux équipements de petit-électroménager', 3, 'X'),
        (N'Au moins deux équipements de petit-électroménager', 4, 'X'),
        (N'Au moins deux équipements de petit-électroménager', 5, 'X'),
        -- Critère 66
        (N'Autocuiseur ou cuir-vapeur ou robot de cuisine multifonctions', 1, 'O'),
        (N'Autocuiseur ou cuir-vapeur ou robot de cuisine multifonctions', 2, 'O'),
        (N'Autocuiseur ou cuir-vapeur ou robot de cuisine multifonctions', 3, 'O'),
        (N'Autocuiseur ou cuir-vapeur ou robot de cuisine multifonctions', 4, 'O'),
        (N'Autocuiseur ou cuir-vapeur ou robot de cuisine multifonctions', 5, 'O'),
        -- Critère 67
        (N'Cafetière', 1, 'X'),
        (N'Cafetière', 2, 'X'),
        (N'Cafetière', 3, 'X'),
        (N'Cafetière', 4, 'X'),
        (N'Cafetière', 5, 'X'),
        -- Critère 68
        (N'Machine à expresso', 1, 'O'),
        (N'Machine à expresso', 2, 'O'),
        (N'Machine à expresso', 3, 'O'),
        (N'Machine à expresso', 4, 'X'),
        (N'Machine à expresso', 5, 'X'),
        -- Critère 69
        (N'Bouilloire', 1, 'O'),
        (N'Bouilloire', 2, 'O'),
        (N'Bouilloire', 3, 'X'),
        (N'Bouilloire', 4, 'X'),
        (N'Bouilloire', 5, 'X'),
        -- Critère 70
        (N'Grille-pain', 1, 'O'),
        (N'Grille-pain', 2, 'O'),
        (N'Grille-pain', 3, 'X'),
        (N'Grille-pain', 4, 'X'),
        (N'Grille-pain', 5, 'X'),
        -- Critère 71
        (N'Lave-vaisselle pour les logements à partir de 2 personnes', 1, 'O'),
        (N'Lave-vaisselle pour les logements à partir de 2 personnes', 2, 'O'),
        (N'Lave-vaisselle pour les logements à partir de 2 personnes', 3, 'O'),
        (N'Lave-vaisselle pour les logements à partir de 2 personnes', 4, 'X'),
        (N'Lave-vaisselle pour les logements à partir de 2 personnes', 5, 'X'),
        -- Critère 72
        (N'Lave-vaisselle de 6 couverts ou plus pour les logements à partir de 4 personnes', 1, 'O'),
        (N'Lave-vaisselle de 6 couverts ou plus pour les logements à partir de 4 personnes', 2, 'O'),
        (N'Lave-vaisselle de 6 couverts ou plus pour les logements à partir de 4 personnes', 3, 'X'),
        (N'Lave-vaisselle de 6 couverts ou plus pour les logements à partir de 4 personnes', 4, 'X'),
        (N'Lave-vaisselle de 6 couverts ou plus pour les logements à partir de 4 personnes', 5, 'X'),
        -- Critère 73
        (N'Réfrigérateur avec compartiment conservateur', 1, 'X'),
        (N'Réfrigérateur avec compartiment conservateur', 2, 'X'),
        (N'Réfrigérateur avec compartiment conservateur', 3, 'X'),
        (N'Réfrigérateur avec compartiment conservateur', 4, 'X'),
        (N'Réfrigérateur avec compartiment conservateur', 5, 'X'),
        -- Critère 74
        (N'Présence d''un congélateur ou compartiment congélateur', 1, 'O'),
        (N'Présence d''un congélateur ou compartiment congélateur', 2, 'O'),
        (N'Présence d''un congélateur ou compartiment congélateur', 3, 'X'),
        (N'Présence d''un congélateur ou compartiment congélateur', 4, 'X'),
        (N'Présence d''un congélateur ou compartiment congélateur', 5, 'X'),
        -- Critère 75
        (N'Poubelle fermée avec couvercle', 1, 'X'),
        (N'Poubelle fermée avec couvercle', 2, 'X'),
        (N'Poubelle fermée avec couvercle', 3, 'X'),
        (N'Poubelle fermée avec couvercle', 4, 'X'),
        (N'Poubelle fermée avec couvercle', 5, 'X'),
        -- Critère 76
        (N'Pour accéder au 4ème étage à partir du rez-de-chaussée', 1, 'X'),
        (N'Pour accéder au 4ème étage à partir du rez-de-chaussée', 2, 'X'),
        (N'Pour accéder au 4ème étage à partir du rez-de-chaussée', 3, 'NA'),
        (N'Pour accéder au 4ème étage à partir du rez-de-chaussée', 4, 'NA'),
        (N'Pour accéder au 4ème étage à partir du rez-de-chaussée', 5, 'NA'),
        -- Critère 77
        (N'Pour accéder au 3ème étage à partir du rez-de-chaussée', 1, 'O'),
        (N'Pour accéder au 3ème étage à partir du rez-de-chaussée', 2, 'O'),
        (N'Pour accéder au 3ème étage à partir du rez-de-chaussée', 3, 'X'),
        (N'Pour accéder au 3ème étage à partir du rez-de-chaussée', 4, 'X'),
        (N'Pour accéder au 3ème étage à partir du rez-de-chaussée', 5, 'X'),
        -- Critère 78
        (N'Emplacement(s) à proximité', 1, 'X'),
        (N'Emplacement(s) à proximité', 2, 'X'),
        (N'Emplacement(s) à proximité', 3, 'X'),
        (N'Emplacement(s) à proximité', 4, 'X'),
        (N'Emplacement(s) à proximité', 5, 'X'),
        -- Critère 79
        (N'Emplacement(s) privatif(s)', 1, 'O'),
        (N'Emplacement(s) privatif(s)', 2, 'O'),
        (N'Emplacement(s) privatif(s)', 3, 'X'),
        (N'Emplacement(s) privatif(s)', 4, 'X'),
        (N'Emplacement(s) privatif(s)', 5, 'X'),
        -- Critère 80
        (N'Garage ou abri couvert privatif', 1, 'O'),
        (N'Garage ou abri couvert privatif', 2, 'O'),
        (N'Garage ou abri couvert privatif', 3, 'O'),
        (N'Garage ou abri couvert privatif', 4, 'O'),
        (N'Garage ou abri couvert privatif', 5, 'O'),
        -- Critère 81
        (N'Logement avec balcon, loggia ou veranda (3m² minimum)', 1, 'O'),
        (N'Logement avec balcon, loggia ou veranda (3m² minimum)', 2, 'O'),
        (N'Logement avec balcon, loggia ou veranda (3m² minimum)', 3, 'O'),
        (N'Logement avec balcon, loggia ou veranda (3m² minimum)', 4, 'O'),
        (N'Logement avec balcon, loggia ou veranda (3m² minimum)', 5, 'O'),
        -- Critère 82
        (N'Logement avec terrasse ou jardin privé (8m² minimum)', 1, 'O'),
        (N'Logement avec terrasse ou jardin privé (8m² minimum)', 2, 'O'),
        (N'Logement avec terrasse ou jardin privé (8m² minimum)', 3, 'O'),
        (N'Logement avec terrasse ou jardin privé (8m² minimum)', 4, 'O'),
        (N'Logement avec terrasse ou jardin privé (8m² minimum)', 5, 'O'),
        -- Critère 83
        (N'Logement avec parc ou jardin (50m² minimum)', 1, 'O'),
        (N'Logement avec parc ou jardin (50m² minimum)', 2, 'O'),
        (N'Logement avec parc ou jardin (50m² minimum)', 3, 'O'),
        (N'Logement avec parc ou jardin (50m² minimum)', 4, 'O'),
        (N'Logement avec parc ou jardin (50m² minimum)', 5, 'O'),
        -- Critère 84
        (N'Présence de mobilier de jardin privatif propre et en bon état', 1, 'O'),
        (N'Présence de mobilier de jardin privatif propre et en bon état', 2, 'O'),
        (N'Présence de mobilier de jardin privatif propre et en bon état', 3, 'O'),
        (N'Présence de mobilier de jardin privatif propre et en bon état', 4, 'O'),
        (N'Présence de mobilier de jardin privatif propre et en bon état', 5, 'O'),
        -- Critère 85
        (N'Mise à disposition d''une planche extérieure et/ou d''un barbecue extérieur', 1, 'O'),
        (N'Mise à disposition d''une planche extérieure et/ou d''un barbecue extérieur', 2, 'O'),
        (N'Mise à disposition d''une planche extérieure et/ou d''un barbecue extérieur', 3, 'O'),
        (N'Mise à disposition d''une planche extérieure et/ou d''un barbecue extérieur', 4, 'O'),
        (N'Mise à disposition d''une planche extérieure et/ou d''un barbecue extérieur', 5, 'O'),
        -- Critère 86
        (N'Un équipement léger de loisirs, détente ou sport, dédié au logement', 1, 'O'),
        (N'Un équipement léger de loisirs, détente ou sport, dédié au logement', 2, 'O'),
        (N'Un équipement léger de loisirs, détente ou sport, dédié au logement', 3, 'O'),
        (N'Un équipement léger de loisirs, détente ou sport, dédié au logement', 4, 'X'),
        (N'Un équipement léger de loisirs, détente ou sport, dédié au logement', 5, 'X'),
        -- Critère 87
        (N'Un équipement aménagé de loisirs, détente ou sport, dédié au logement', 1, 'O'),
        (N'Un équipement aménagé de loisirs, détente ou sport, dédié au logement', 2, 'O'),
        (N'Un équipement aménagé de loisirs, détente ou sport, dédié au logement', 3, 'O'),
        (N'Un équipement aménagé de loisirs, détente ou sport, dédié au logement', 4, 'O'),
        (N'Un équipement aménagé de loisirs, détente ou sport, dédié au logement', 5, 'O'),
        -- Critère 88
        (N'Piscine extérieure ou intérieure', 1, 'O'),
        (N'Piscine extérieure ou intérieure', 2, 'O'),
        (N'Piscine extérieure ou intérieure', 3, 'O'),
        (N'Piscine extérieure ou intérieure', 4, 'O'),
        (N'Piscine extérieure ou intérieure', 5, 'O'),
        -- Critère 89
        (N'Piscine extérieure ou intérieure chauffée', 1, 'O'),
        (N'Piscine extérieure ou intérieure chauffée', 2, 'O'),
        (N'Piscine extérieure ou intérieure chauffée', 3, 'O'),
        (N'Piscine extérieure ou intérieure chauffée', 4, 'O'),
        (N'Piscine extérieure ou intérieure chauffée', 5, 'O'),
        -- Critère 90
        (N'Existence de rangement(s) pour équipement sportif', 1, 'O'),
        (N'Existence de rangement(s) pour équipement sportif', 2, 'O'),
        (N'Existence de rangement(s) pour équipement sportif', 3, 'O'),
        (N'Existence de rangement(s) pour équipement sportif', 4, 'O'),
        (N'Existence de rangement(s) pour équipement sportif', 5, 'O'),
        -- Critère 91
        (N'Logement avec vue paysagère (vue mer, montagne, plaine ou zone urbaine)', 1, 'O'),
        (N'Logement avec vue paysagère (vue mer, montagne, plaine ou zone urbaine)', 2, 'O'),
        (N'Logement avec vue paysagère (vue mer, montagne, plaine ou zone urbaine)', 3, 'O'),
        (N'Logement avec vue paysagère (vue mer, montagne, plaine ou zone urbaine)', 4, 'O'),
        (N'Logement avec vue paysagère (vue mer, montagne, plaine ou zone urbaine)', 5, 'O'),
        -- Critère 92
        (N'Logement avec accès immédiat à un environnement offrant la possibilité de faire des activités : nature, culture et sport', 1, 'O'),
        (N'Logement avec accès immédiat à un environnement offrant la possibilité de faire des activités : nature, culture et sport', 2, 'O'),
        (N'Logement avec accès immédiat à un environnement offrant la possibilité de faire des activités : nature, culture et sport', 3, 'O'),
        (N'Logement avec accès immédiat à un environnement offrant la possibilité de faire des activités : nature, culture et sport', 4, 'O'),
        (N'Logement avec accès immédiat à un environnement offrant la possibilité de faire des activités : nature, culture et sport', 5, 'O'),
        -- Critère 93
        (N'Logement avec accès immédiat aux commerces, services et transports en commun', 1, 'O'),
        (N'Logement avec accès immédiat aux commerces, services et transports en commun', 2, 'O'),
        (N'Logement avec accès immédiat aux commerces, services et transports en commun', 3, 'O'),
        (N'Logement avec accès immédiat aux commerces, services et transports en commun', 4, 'O'),
        (N'Logement avec accès immédiat aux commerces, services et transports en commun', 5, 'O'),
        -- Critère 94
        (N'Les sanitaires (toilette(s) et salle(s) d''eau) sont propres et en bon état', 1, 'X_ONC'),
        (N'Les sanitaires (toilette(s) et salle(s) d''eau) sont propres et en bon état', 2, 'X_ONC'),
        (N'Les sanitaires (toilette(s) et salle(s) d''eau) sont propres et en bon état', 3, 'X_ONC'),
        (N'Les sanitaires (toilette(s) et salle(s) d''eau) sont propres et en bon état', 4, 'X_ONC'),
        (N'Les sanitaires (toilette(s) et salle(s) d''eau) sont propres et en bon état', 5, 'X_ONC'),
        -- Critère 95
        (N'Les sols murs et plafonds sont propres et en bon état', 1, 'X_ONC'),
        (N'Les sols murs et plafonds sont propres et en bon état', 2, 'X_ONC'),
        (N'Les sols murs et plafonds sont propres et en bon état', 3, 'X_ONC'),
        (N'Les sols murs et plafonds sont propres et en bon état', 4, 'X_ONC'),
        (N'Les sols murs et plafonds sont propres et en bon état', 5, 'X_ONC'),
        -- Critère 96
        (N'Le mobilier est propre et en bon état', 1, 'X_ONC'),
        (N'Le mobilier est propre et en bon état', 2, 'X_ONC'),
        (N'Le mobilier est propre et en bon état', 3, 'X_ONC'),
        (N'Le mobilier est propre et en bon état', 4, 'X_ONC'),
        (N'Le mobilier est propre et en bon état', 5, 'X_ONC'),
        -- Critère 97
        (N'La literie est propre et en bon état', 1, 'X_ONC'),
        (N'La literie est propre et en bon état', 2, 'X_ONC'),
        (N'La literie est propre et en bon état', 3, 'X_ONC'),
        (N'La literie est propre et en bon état', 4, 'X_ONC'),
        (N'La literie est propre et en bon état', 5, 'X_ONC'),
        -- Critère 98
        (N'La cuisine ou coin cuisine et les équipements sont propres et en bon état', 1, 'X_ONC'),
        (N'La cuisine ou coin cuisine et les équipements sont propres et en bon état', 2, 'X_ONC'),
        (N'La cuisine ou coin cuisine et les équipements sont propres et en bon état', 3, 'X_ONC'),
        (N'La cuisine ou coin cuisine et les équipements sont propres et en bon état', 4, 'X_ONC'),
        (N'La cuisine ou coin cuisine et les équipements sont propres et en bon état', 5, 'X_ONC'),
        -- Critère 99
        (N'Mise à disposition de brochures d''informations locales et touristiques en français et dans au moins une langue étrangère', 1, 'X'),
        (N'Mise à disposition de brochures d''informations locales et touristiques en français et dans au moins une langue étrangère', 2, 'X'),
        (N'Mise à disposition de brochures d''informations locales et touristiques en français et dans au moins une langue étrangère', 3, 'X'),
        (N'Mise à disposition de brochures d''informations locales et touristiques en français et dans au moins une langue étrangère', 4, 'X'),
        (N'Mise à disposition de brochures d''informations locales et touristiques en français et dans au moins une langue étrangère', 5, 'X'),
        -- Critère 100
        (N'Mise à disposition d''un livret d''accueil', 1, 'O'),
        (N'Mise à disposition d''un livret d''accueil', 2, 'O'),
        (N'Mise à disposition d''un livret d''accueil', 3, 'X'),
        (N'Mise à disposition d''un livret d''accueil', 4, 'X'),
        (N'Mise à disposition d''un livret d''accueil', 5, 'X'),
        -- Critère 101
        (N'Accueil sur place par le propriétaire ou son représentant', 1, 'O'),
        (N'Accueil sur place par le propriétaire ou son représentant', 2, 'X'),
        (N'Accueil sur place par le propriétaire ou son représentant', 3, 'X'),
        (N'Accueil sur place par le propriétaire ou son représentant', 4, 'X'),
        (N'Accueil sur place par le propriétaire ou son représentant', 5, 'X'),
        -- Critère 102
        (N'Cadeau de bienvenue à l''arrivée du client', 1, 'O'),
        (N'Cadeau de bienvenue à l''arrivée du client', 2, 'O'),
        (N'Cadeau de bienvenue à l''arrivée du client', 3, 'O'),
        (N'Cadeau de bienvenue à l''arrivée du client', 4, 'X'),
        (N'Cadeau de bienvenue à l''arrivée du client', 5, 'X'),
        -- Critère 103
        (N'Existence d''une boite à clé ou système équivalent', 1, 'O'),
        (N'Existence d''une boite à clé ou système équivalent', 2, 'O'),
        (N'Existence d''une boite à clé ou système équivalent', 3, 'O'),
        (N'Existence d''une boite à clé ou système équivalent', 4, 'O'),
        (N'Existence d''une boite à clé ou système équivalent', 5, 'O'),
        -- Critère 104
        (N'Draps de lit proposés systématiquement par le loueur', 1, 'X'),
        (N'Draps de lit proposés systématiquement par le loueur', 2, 'X'),
        (N'Draps de lit proposés systématiquement par le loueur', 3, 'X'),
        (N'Draps de lit proposés systématiquement par le loueur', 4, 'X'),
        (N'Draps de lit proposés systématiquement par le loueur', 5, 'X'),
        -- Critère 105
        (N'Linge de toilette proposé systématiquement par le loueur', 1, 'X'),
        (N'Linge de toilette proposé systématiquement par le loueur', 2, 'X'),
        (N'Linge de toilette proposé systématiquement par le loueur', 3, 'X'),
        (N'Linge de toilette proposé systématiquement par le loueur', 4, 'X'),
        (N'Linge de toilette proposé systématiquement par le loueur', 5, 'X'),
        -- Critère 106
        (N'Linge de table', 1, 'O'),
        (N'Linge de table', 2, 'O'),
        (N'Linge de table', 3, 'X'),
        (N'Linge de table', 4, 'X'),
        (N'Linge de table', 5, 'X'),
        -- Critère 107
        (N'Lits faits à l''arrivée proposés systématiquement par le loueur', 1, 'O'),
        (N'Lits faits à l''arrivée proposés systématiquement par le loueur', 2, 'O'),
        (N'Lits faits à l''arrivée proposés systématiquement par le loueur', 3, 'X'),
        (N'Lits faits à l''arrivée proposés systématiquement par le loueur', 4, 'X'),
        (N'Lits faits à l''arrivée proposés systématiquement par le loueur', 5, 'X'),
        -- Critère 108
        (N'Matériel pour bébé à la demande', 1, 'O'),
        (N'Matériel pour bébé à la demande', 2, 'O'),
        (N'Matériel pour bébé à la demande', 3, 'X'),
        (N'Matériel pour bébé à la demande', 4, 'X'),
        (N'Matériel pour bébé à la demande', 5, 'X'),
        -- Critère 109
        (N'Service de ménage proposé systématiquement', 1, 'O'),
        (N'Service de ménage proposé systématiquement', 2, 'O'),
        (N'Service de ménage proposé systématiquement', 3, 'X'),
        (N'Service de ménage proposé systématiquement', 4, 'X'),
        (N'Service de ménage proposé systématiquement', 5, 'X'),
        -- Critère 110
        (N'Présence de produits d''entretien', 1, 'X'),
        (N'Présence de produits d''entretien', 2, 'X'),
        (N'Présence de produits d''entretien', 3, 'X'),
        (N'Présence de produits d''entretien', 4, 'X'),
        (N'Présence de produits d''entretien', 5, 'X'),
        -- Critère 111
        (N'Adaptateurs électriques', 1, 'O'),
        (N'Adaptateurs électriques', 2, 'O'),
        (N'Adaptateurs électriques', 3, 'X'),
        (N'Adaptateurs électriques', 4, 'X'),
        (N'Adaptateurs électriques', 5, 'X'),
        -- Critère 112
        (N'Existence d''un site internet ou d''une page internet dédiée au logement', 1, 'O'),
        (N'Existence d''un site internet ou d''une page internet dédiée au logement', 2, 'O'),
        (N'Existence d''un site internet ou d''une page internet dédiée au logement', 3, 'X'),
        (N'Existence d''un site internet ou d''une page internet dédiée au logement', 4, 'X'),
        (N'Existence d''un site internet ou d''une page internet dédiée au logement', 5, 'X'),
        -- Critère 113
        (N'Existence d''un site internet ou d''une page internet dédiée au logement en une langue étrangère', 1, 'O'),
        (N'Existence d''un site internet ou d''une page internet dédiée au logement en une langue étrangère', 2, 'O'),
        (N'Existence d''un site internet ou d''une page internet dédiée au logement en une langue étrangère', 3, 'X'),
        (N'Existence d''un site internet ou d''une page internet dédiée au logement en une langue étrangère', 4, 'X'),
        (N'Existence d''un site internet ou d''une page internet dédiée au logement en une langue étrangère', 5, 'X'),
        -- Critère 114
        (N'Animaux de compagnie admis', 1, 'O'),
        (N'Animaux de compagnie admis', 2, 'O'),
        (N'Animaux de compagnie admis', 3, 'O'),
        (N'Animaux de compagnie admis', 4, 'O'),
        (N'Animaux de compagnie admis', 5, 'O'),
        -- Critère 115
        (N'Informations concernant l''accessibilité sur les supports d''information', 1, 'X'),
        (N'Informations concernant l''accessibilité sur les supports d''information', 2, 'X'),
        (N'Informations concernant l''accessibilité sur les supports d''information', 3, 'X'),
        (N'Informations concernant l''accessibilité sur les supports d''information', 4, 'X'),
        (N'Informations concernant l''accessibilité sur les supports d''information', 5, 'X'),
        -- Critère 116
        (N'Mise à disposition de télécommande de télévision à grosses touches et de couleurs contrastées', 1, 'O'),
        (N'Mise à disposition de télécommande de télévision à grosses touches et de couleurs contrastées', 2, 'O'),
        (N'Mise à disposition de télécommande de télévision à grosses touches et de couleurs contrastées', 3, 'O'),
        (N'Mise à disposition de télécommande de télévision à grosses touches et de couleurs contrastées', 4, 'O'),
        (N'Mise à disposition de télécommande de télévision à grosses touches et de couleurs contrastées', 5, 'O'),
        -- Critère 117
        (N'Présence d''un siège de douche avec barre d''appui', 1, 'O'),
        (N'Présence d''un siège de douche avec barre d''appui', 2, 'O'),
        (N'Présence d''un siège de douche avec barre d''appui', 3, 'O'),
        (N'Présence d''un siège de douche avec barre d''appui', 4, 'O'),
        (N'Présence d''un siège de douche avec barre d''appui', 5, 'O'),
        -- Critère 118
        (N'Présence de WC avec barre d''appui', 1, 'O'),
        (N'Présence de WC avec barre d''appui', 2, 'O'),
        (N'Présence de WC avec barre d''appui', 3, 'O'),
        (N'Présence de WC avec barre d''appui', 4, 'O'),
        (N'Présence de WC avec barre d''appui', 5, 'O'),
        -- Critère 119
        (N'Largeur de toutes les portes adaptées', 1, 'O'),
        (N'Largeur de toutes les portes adaptées', 2, 'O'),
        (N'Largeur de toutes les portes adaptées', 3, 'O'),
        (N'Largeur de toutes les portes adaptées', 4, 'O'),
        (N'Largeur de toutes les portes adaptées', 5, 'O'),
        -- Critère 120
        (N'Document accessible mis à disposition', 1, 'X'),
        (N'Document accessible mis à disposition', 2, 'X'),
        (N'Document accessible mis à disposition', 3, 'X'),
        (N'Document accessible mis à disposition', 4, 'X'),
        (N'Document accessible mis à disposition', 5, 'X'),
        -- Critère 121
        (N'Obtention du label Tourisme et Handicap', 1, 'O'),
        (N'Obtention du label Tourisme et Handicap', 2, 'O'),
        (N'Obtention du label Tourisme et Handicap', 3, 'O'),
        (N'Obtention du label Tourisme et Handicap', 4, 'O'),
        (N'Obtention du label Tourisme et Handicap', 5, 'O'),
        -- Critère 122
        (N'Mise en œuvre d''une mesure de réduction de consommation d''énergie', 1, 'X'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''énergie', 2, 'X'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''énergie', 3, 'X'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''énergie', 4, 'X'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''énergie', 5, 'X'),
        -- Critère 123
        (N'Mise en œuvre d''une mesure de réduction de consommation d''énergie supplémentaire', 1, 'O'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''énergie supplémentaire', 2, 'O'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''énergie supplémentaire', 3, 'O'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''énergie supplémentaire', 4, 'O'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''énergie supplémentaire', 5, 'O'),
        -- Critère 124
        (N'Borne de recharge pour les véhicules électriques', 1, 'O'),
        (N'Borne de recharge pour les véhicules électriques', 2, 'O'),
        (N'Borne de recharge pour les véhicules électriques', 3, 'O'),
        (N'Borne de recharge pour les véhicules électriques', 4, 'O'),
        (N'Borne de recharge pour les véhicules électriques', 5, 'O'),
        -- Critère 125
        (N'Mise en œuvre d''une mesure de réduction de consommation d''eau', 1, 'X'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''eau', 2, 'X'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''eau', 3, 'X'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''eau', 4, 'X'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''eau', 5, 'X'),
        -- Critère 126
        (N'Mise en œuvre d''une mesure de réduction de consommation d''eau supplémentaire', 1, 'O'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''eau supplémentaire', 2, 'O'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''eau supplémentaire', 3, 'O'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''eau supplémentaire', 4, 'O'),
        (N'Mise en œuvre d''une mesure de réduction de consommation d''eau supplémentaire', 5, 'O'),
        -- Critère 127
        (N'Existence d''un système de tri des déchets dédié au logement', 1, 'X'),
        (N'Existence d''un système de tri des déchets dédié au logement', 2, 'X'),
        (N'Existence d''un système de tri des déchets dédié au logement', 3, 'X'),
        (N'Existence d''un système de tri des déchets dédié au logement', 4, 'X'),
        (N'Existence d''un système de tri des déchets dédié au logement', 5, 'X'),
        -- Critère 128
        (N'Existence d''un composteur', 1, 'O'),
        (N'Existence d''un composteur', 2, 'O'),
        (N'Existence d''un composteur', 3, 'O'),
        (N'Existence d''un composteur', 4, 'O'),
        (N'Existence d''un composteur', 5, 'O'),
        -- Critère 129
        (N'Sensibilisation des clients sur les actions qu''ils peuvent réaliser lors de leur séjour en matière de respect de l''environnement', 1, 'X'),
        (N'Sensibilisation des clients sur les actions qu''ils peuvent réaliser lors de leur séjour en matière de respect de l''environnement', 2, 'X'),
        (N'Sensibilisation des clients sur les actions qu''ils peuvent réaliser lors de leur séjour en matière de respect de l''environnement', 3, 'X'),
        (N'Sensibilisation des clients sur les actions qu''ils peuvent réaliser lors de leur séjour en matière de respect de l''environnement', 4, 'X'),
        (N'Sensibilisation des clients sur les actions qu''ils peuvent réaliser lors de leur séjour en matière de respect de l''environnement', 5, 'X'),
        -- Critère 130
        (N'Présence de produits d''accueil écologiques dans la salle de bains', 1, 'O'),
        (N'Présence de produits d''accueil écologiques dans la salle de bains', 2, 'O'),
        (N'Présence de produits d''accueil écologiques dans la salle de bains', 3, 'O'),
        (N'Présence de produits d''accueil écologiques dans la salle de bains', 4, 'O'),
        (N'Présence de produits d''accueil écologiques dans la salle de bains', 5, 'O'),
        -- Critère 131
        (N'Mise à disposition d''au moins deux produits d''entretien respectueux de l''environnement', 1, 'X'),
        (N'Mise à disposition d''au moins deux produits d''entretien respectueux de l''environnement', 2, 'X'),
        (N'Mise à disposition d''au moins deux produits d''entretien respectueux de l''environnement', 3, 'X'),
        (N'Mise à disposition d''au moins deux produits d''entretien respectueux de l''environnement', 4, 'X'),
        (N'Mise à disposition d''au moins deux produits d''entretien respectueux de l''environnement', 5, 'X'),
        -- Critère 132
        (N'Obtention d''un label environnemental', 1, 'O'),
        (N'Obtention d''un label environnemental', 2, 'O'),
        (N'Obtention d''un label environnemental', 3, 'O'),
        (N'Obtention d''un label environnemental', 4, 'O'),
        (N'Obtention d''un label environnemental', 5, 'O')
    ) AS x(Libelle, StarLevel, StatusCode)
)
INSERT INTO dbo.StarLevelCriterion ([StarLevelId], [CriterionId], [TypeCode])
SELECT sl.StarLevelId, c.CriterionId, cs.StatusCode
FROM CritStat cs
INNER JOIN dbo.Criterion c ON c.[Description] = cs.Libelle
INNER JOIN dbo.StarLevel sl ON sl.StarLevelId = cs.StarLevel
WHERE NOT EXISTS (
    SELECT 1 FROM dbo.StarLevelCriterion slc
    WHERE slc.StarLevelId = sl.StarLevelId AND slc.CriterionId = c.CriterionId
);
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'FolderStatus')
BEGIN
    INSERT INTO dbo.FolderStatus ([Identifier], [Label])
    SELECT x.[Identifier], x.[Label]
    FROM (VALUES
        (1, N'En cours'),
        (2, N'En attente de devis'),
        (3, N'En attente de paiement'),
        (4, N'Terminé'),
        (5, N'Annulé')
    ) AS x([Identifier], [Label])
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.FolderStatus fs
        WHERE fs.[Identifier] = x.[Identifier]
    );
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'AccommodationType')
BEGIN
    INSERT INTO dbo.AccommodationType ([Identifier], [Label], [Description])
    SELECT x.[Identifier], x.[Label], x.[Description]
    FROM (VALUES
        (1, N'Appartement', N'Logement en immeuble collectif'),
        (2, N'Maison', N'Logement individuel'),
        (3, N'Hôtel', N'Établissement hôtelier'),
        (4, N'Meublé de tourisme', N'Location saisonnière')
    ) AS x([Identifier], [Label], [Description])
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.AccommodationType at
        WHERE at.[Identifier] = x.[Identifier]
    );
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'Accommodation')
BEGIN
    INSERT INTO dbo.Accommodation
        ([Identifier], [AccommodationName], [AccommodationTypeIdentifier], [AddressIdentifier], [IsActive], [CreatedDate])
    SELECT
        x.[Identifier],
        x.[AccommodationName],
        x.[AccommodationTypeIdentifier],
        x.[AddressIdentifier],
        x.[IsActive],
        x.[CreatedDate]
    FROM (VALUES
        (1, 'Appartement exemple', 1, 1, 1, GETDATE()),
        (2, 'Maison exemple', 2, 2, 1, GETDATE())
    ) AS x([Identifier], [AccommodationName], [AccommodationTypeIdentifier], [AddressIdentifier], [IsActive], [CreatedDate])
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.Accommodation a
        WHERE a.[Identifier] = x.[Identifier]
    );
END
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES 
           WHERE TABLE_SCHEMA = 'dbo' 
           AND TABLE_NAME = 'Folder')
BEGIN
    INSERT INTO dbo.Folder
        ([Identifier], [AccommodationTypeIdentifier], [AccommodationIdentifier], [OwnerUserIdentifier], [InspectorUserIdentifier], 
        [FolderStatusIdentifier], [CreatedDate])
    SELECT
        1, 1, 1, 1, 2, 1, GETDATE()
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.Folder f
        WHERE f.[Identifier] = 1
    );
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Assessment')
BEGIN
    CREATE TABLE [dbo].[Assessment] (
        [Identifier]          INT             IDENTITY(1,1) NOT NULL,
        [FolderIdentifier]    INT             NOT NULL,
        [TargetStarLevel]     TINYINT         NOT NULL,        -- Étoile visée
        [Capacity]            INT             NOT NULL,        -- Capacité d’accueil max
        [NumberOfFloors]      INT             NOT NULL,        -- Nombre d’étages
        [IsWhiteZone]         BIT             NOT NULL DEFAULT 0,
        [IsDromTom]           BIT             NOT NULL DEFAULT 0,
        [IsHighMountain]      BIT             NOT NULL DEFAULT 0,
        [IsBuildingClassified] BIT            NOT NULL DEFAULT 0,
        [IsStudioNoLivingRoom] BIT            NOT NULL DEFAULT 0,
        [IsParkingImpossible] BIT             NOT NULL DEFAULT 0,
        [TotalArea]           DECIMAL(10,2)   NOT NULL,        -- Superficie totale (m²)
        [NumberOfRooms]       INT             NOT NULL,        -- Nombre de chambres
        [TotalRoomsArea]      DECIMAL(10,2)   NOT NULL,        -- Somme des surfaces des chambres
        [SmallestRoomArea]    DECIMAL(10,2)   NOT NULL,        -- Plus petite chambre (m²)
        [CreatedDate]         DATETIME        NOT NULL DEFAULT GETDATE(),
        [UpdatedDate]         DATETIME        NULL,
        [IsComplete]          BIT             NOT NULL DEFAULT 0, -- Évaluation terminée

        CONSTRAINT [PK_Assessment] PRIMARY KEY ([Identifier]),

        CONSTRAINT [FK_Assessment_Folder] 
            FOREIGN KEY ([FolderIdentifier]) REFERENCES [Folder]([Identifier]),

        CONSTRAINT [FK_Assessment_StarLevel] 
            FOREIGN KEY ([TargetStarLevel]) REFERENCES [StarLevel]([StarLevelId])
    );
END
GO

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'AssessmentCriterion')
BEGIN
    CREATE TABLE [dbo].[AssessmentCriterion] (
        [AssessmentIdentifier] INT          NOT NULL,
        [CriterionId]          INT          NOT NULL,
        [Points]               INT          NOT NULL,   -- Points obtenus pour ce critère
        [Status]               VARCHAR(10)  NOT NULL,   -- ex: 'X', 'O', 'NA', 'X_ONC'
        [IsValidated]          BIT          NOT NULL DEFAULT 0,
        [Comment]              VARCHAR(500) NULL,

        CONSTRAINT [PK_AssessmentCriterion] 
            PRIMARY KEY ([AssessmentIdentifier], [CriterionId]),

        CONSTRAINT [FK_AssessmentCriterion_Assessment] 
            FOREIGN KEY ([AssessmentIdentifier]) REFERENCES [Assessment]([Identifier]),

        CONSTRAINT [FK_AssessmentCriterion_Criterion] 
            FOREIGN KEY ([CriterionId]) REFERENCES [Criterion]([CriterionId])
    );
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Appointment'
)
BEGIN
    CREATE TABLE [dbo].[Appointment] (
        [Identifier] INT NOT NULL,
        [AppointmentDate] DATETIME NOT NULL,
        [AddressIdentifier] INT NULL,
        [Comment] VARCHAR(255) NULL,
        [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
        [UpdatedDate] DATETIME NULL,

        CONSTRAINT [PK_Appointment] PRIMARY KEY ([Identifier]),

        CONSTRAINT [FK_Appointement_Address]
            FOREIGN KEY ([AddressIdentifier])
            REFERENCES [Address]([Identifier])
    );
END
GO

IF NOT EXISTS (
    SELECT 1
    FROM INFORMATION_SCHEMA.TABLES
    WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'AssessmentResult'
)
BEGIN
    CREATE TABLE [dbo].[AssessmentResult] (
    [Identifier] INT NOT NULL,
    [AssesmentIdentifier] int NOT NULL,
    [IsAccepted] BIT NOT NULL,
    [MandatoryPointsEarned] INT NOT NULL,
    [MandatoryThreshold] INT NOT NULL,
    [OptionalPointsEarned] INT NOT NULL,
    [OptionalRequired] INT NOT NULL,
    [OncFailedCount] INT NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETDATE(),
    [UpdatedDate] DATETIME NULL,

    CONSTRAINT [PK_AssessmentResult] PRIMARY KEY ([Identifier]),

    CONSTRAINT [FK_AssessmentResult_Assessment] 
        FOREIGN KEY ([AssesmentIdentifier]) 
        REFERENCES [Assessment]([Identifier])
    );

END
GO