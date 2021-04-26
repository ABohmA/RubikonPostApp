# RubikonPostApp

BlogPost zadatak za rubikon

U sklopu istrazivanja za dati zadatak pronasao sam zadatak koji ste mi poslali u potpunosti uradjen na GITHUBU te sam odlucio da oprobam napraviti drugaciji pristup od vec postojeceg. Naredni link je link uradjenog zadatka https://github.com/AnnieMonk/BloggingApp. U svrhu izbjegavanja forme koju sam pronasao na internetu nadam se da mi necete uzeti za zlo pokusaj da zadtak uradim na malo drugaciji nacin.

Po zahtijevu unutar zadatka su uradjene sve trazene funkcije te unutar samog koda svaka metoda ima dodatna objasnjenja tamo gdje je to potrebno

Kako bi bili u mogucnosti da zadatak pokrenete potrebno je promjeniti connection string na vas lokalni server. U sklopu projekta cu dodati SQL scriptu za generisanje baze kao i backup baze sa osnovnim podacima.

Iz zadatka su napravljenje sljedece metode koje mozete testirati putem postmana

"api/Posts/GetBySlug/{slug}" Uz pomoc prosljedjenog sluga dohvata post "api/Posts/GetAll/{tag?}" dohvata sve postove od najnovijeg prema najstarijem ( filtriranje po tagu je opcionalno)

"api/Posts/post" postavlja novi post u bazu Forma za dodavanje novog posta kroz postman: {

"Title" : "Novi naslov !",

"PostDescription" : "Ovo je testni opis",

"Body": "Ovo je testni body za post pod imenom novi naslov",

"Tagovi" : ["C#", "Angular"]

}

"api/Posts/Update" mijenja postojeci post i sve izmjene unutar posta su opcionalne osim Sluga(on se ne moze mijenjati jer u ovome slucaju sluzi kao primarni kljuc) Forma za izmjenu postojeceg posta kroz postman: { "Slug" : "zadnji-test-prije-slanja-qkvysuhm7d",

"Title" : "Zadnji test prije slanja izmjenaaaa",

"PostDescription" : "Ovo je zadnji test",

"Body": "Ovo je testni body za post pod imenom Zadnji test za slanje u svrhu testiranja svih metoda" ,

"Tagovi" : ["SQL", "Angular"]

}

"api/Posts/Del/{slug}" Uz pomoc prosljedjenog sluga brise post i sve reference prema tabeli tagovi

"api/Tags" vraca listu svih tagova
