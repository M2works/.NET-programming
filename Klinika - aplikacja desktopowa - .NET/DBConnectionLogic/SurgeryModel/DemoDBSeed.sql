insert into ClinicDB.dbo.Clinics
	values ('St Martin', 'Poland', 'Warsaw', 'Waryñskiego', 10 , 12, 12345),
	 ('St Tom', 'Poland', 'Poznañ', 'Pi³sudskiego', 10 , 13, 11111),
	 ('St Michael', 'Poland', 'Kraków', 'Marsza³kowska', 10 , 14, 12121),
	 ('Old fagots', 'England', 'London',  'Beer St.',10 , 17, 31000),
	 ('St Roman', 'Poland', 'Gdynia', 'Waryñskiego', 10 , 12, 10111),
	 ('St Bernard', 'Poland', 'Bia³ystok',  'Waryñskiego',10 , 15, 13213)

insert into ClinicDB.dbo.Doctors
	values('Tomasz' , 'Raczek' , 6 , 2, 100,'23456'),
		('Robert' , 'Mak³owicz' , 2 , 3, 80, '65465'),
		('Pawe³' , 'Nastula' , 3 , 1, 120, '67543'),
		('Adam' , 'Ma³ysz' , 1 , 4, 100, '34543'),
		('Adam' , 'Mulczewski' , 4 , 5, 40, '12376'),
		('Tomasz' , 'Karczek' , 5 , 6, 200, '54387'),
		('Marcin' , 'Gortat' , 3 , 7 , 1 , '55476')
		

insert into ClinicDB.dbo.Patients
	values('Tomek', 'M' , '111222333' , '12312305467', 'Poland', 'Warsaw', 'Waryñskiego',10, 12 , 12345),
		('Tomek', 'K' , '111222333' , '12312305467', 'Poland',  'Poznañ', 'Pi³sudskiego',10 , 13, 11111),
		('Tomek', 'P' , '111222333' , '12312306467', 'Poland', 'Kraków', 'Marsza³kowska', 10, 14, 12121),
		('Tomek', 'L' , '111222333' , '12312307467', 'Poland', 'London', 'Beer St.', 10 , 17 , 31000),
		('Tomek', 'N' , '111222333' , '12312308467', 'Poland', 'Gdynia', 'Waryñskiego', 10 , 12 , 10111),
		('Tomek', 'H' , '111222333' , '12312309467', 'Poland', 'Warsaw', 'Waryñskiego',10 , 12, 12345)


insert into ClinicDB.dbo.Timetables
	values('20120618 9:45:00 AM', 7, 3), 
		('20170618 10:30:00 AM', 1, 2), 
		('20150618 11:45:00 AM', 3, 4), 
		('20180718 7:15:00 AM', 4, 6), 
		('20190718 8:15:00 AM', 3, 1), 
		('20130718 9:30:00 AM', 6, 4), 
		('20120618 8:45:00 AM', 5, 5), 
		('20110918 9:15:00 AM', 3, 4), 
		('20100318 11:30:00 AM', 7, 2) 