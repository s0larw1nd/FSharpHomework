:-dynamic high/2.
read_str(A):-get0(X),r_str(X,A,[]).
r_str(10,A,A):-!.
r_str(X,A,B):-append(B,[X],B1),get0(X1),r_str(X1,A,B1).
high_r(X,Y):-	repeat, (high(X,Y) -> (put(32),write(X),nl,write(Y),write("."),nl,
				retract(high(X,Y))) ;X=nil,Y=nil).
pr2:-tell('c:/Prolog/29_1_prolog_F/111.txt'),high_r(X,_),X=nil,told.
pr3:-see('c:/Prolog/29_1_prolog_F/111.txt'),get0(Sym),read_high(Sym),seen.
read_high(-1):-!.
read_high(_):-	read_str(Lang),name(X,Lang),read(Y),asserta(high(X,Y)),
				get0(Sym),read_high(Sym).

after2000("Titanic", 0).
after2000("The Godfather", 0).
after2000("Inception", 3).
after2000("The Matrix", 0).
after2000("The Shawshank Redemption", 0).
after2000("La La Land", 3).
after2000("Gladiator", 0).
after2000("The Dark Knight", 3).
after2000("Forrest Gump", 0).
after2000("Spirited Away", 3).
after2000("Parasite", 3).
after2000("Pulp Fiction", 0).
after2000("The Social Network", 3).
after2000("Schindler’s List", 0).
after2000("The Avengers", 3).
after2000("Avatar", 3).
after2000("The Lion King", 0).
after2000("Django Unchained", 3).
after2000("Interstellar", 3).
after2000("The Silence of the Lambs", 0).

inEnglish("Titanic", 3).
inEnglish("The Godfather", 3).
inEnglish("Inception", 3).
inEnglish("The Matrix", 3).
inEnglish("The Shawshank Redemption", 3).
inEnglish("La La Land", 3).
inEnglish("Gladiator", 3).
inEnglish("The Dark Knight", 3).
inEnglish("Forrest Gump", 3).
inEnglish("Spirited Away", 0).
inEnglish("Parasite", 0).
inEnglish("Pulp Fiction", 3).
inEnglish("The Social Network", 3).
inEnglish("Schindler’s List", 3).
inEnglish("The Avengers", 3).
inEnglish("Avatar", 3).
inEnglish("The Lion King", 3).
inEnglish("Django Unchained", 3).
inEnglish("Interstellar", 3).
inEnglish("The Silence of the Lambs", 3).

basedOnTrueStory("Titanic", 2).
basedOnTrueStory("The Godfather", 0).
basedOnTrueStory("Inception", 0).
basedOnTrueStory("The Matrix", 0).
basedOnTrueStory("The Shawshank Redemption", 0).
basedOnTrueStory("La La Land", 0).
basedOnTrueStory("Gladiator", 2).
basedOnTrueStory("The Dark Knight", 0).
basedOnTrueStory("Forrest Gump", 1).
basedOnTrueStory("Spirited Away", 0).
basedOnTrueStory("Parasite", 0).
basedOnTrueStory("Pulp Fiction", 0).
basedOnTrueStory("The Social Network", 3).
basedOnTrueStory("Schindler’s List", 3).
basedOnTrueStory("The Avengers", 0).
basedOnTrueStory("Avatar", 0).
basedOnTrueStory("The Lion King", 0).
basedOnTrueStory("Django Unchained", 0).
basedOnTrueStory("Interstellar", 0).
basedOnTrueStory("The Silence of the Lambs", 0).

sciFi("Titanic", 0).
sciFi("The Godfather", 0).
sciFi("Inception", 3).
sciFi("The Matrix", 3).
sciFi("The Shawshank Redemption", 0).
sciFi("La La Land", 0).
sciFi("Gladiator", 0).
sciFi("The Dark Knight", 0).
sciFi("Forrest Gump", 0).
sciFi("Spirited Away", 1).
sciFi("Parasite", 0).
sciFi("Pulp Fiction", 0).
sciFi("The Social Network", 0).
sciFi("Schindler’s List", 0).
sciFi("The Avengers", 3).
sciFi("Avatar", 3).
sciFi("The Lion King", 0).
sciFi("Django Unchained", 0).
sciFi("Interstellar", 3).
sciFi("The Silence of the Lambs", 1).

hasComedy("Titanic", 1).
hasComedy("The Godfather", 0).
hasComedy("Inception", 0).
hasComedy("The Matrix", 0).
hasComedy("The Shawshank Redemption", 0).
hasComedy("La La Land", 2).
hasComedy("Gladiator", 0).
hasComedy("The Dark Knight", 1).
hasComedy("Forrest Gump", 2).
hasComedy("Spirited Away", 1).
hasComedy("Parasite", 2).
hasComedy("Pulp Fiction", 2).
hasComedy("The Social Network", 1).
hasComedy("Schindler’s List", 0).
hasComedy("The Avengers", 2).
hasComedy("Avatar", 1).
hasComedy("The Lion King", 2).
hasComedy("Django Unchained", 1).
hasComedy("Interstellar", 0).
hasComedy("The Silence of the Lambs", 0).

hasOscar("Titanic", 3).
hasOscar("The Godfather", 3).
hasOscar("Inception", 3).
hasOscar("The Matrix", 3).
hasOscar("The Shawshank Redemption", 1).
hasOscar("La La Land", 3).
hasOscar("Gladiator", 3).
hasOscar("The Dark Knight", 3).
hasOscar("Forrest Gump", 3).
hasOscar("Spirited Away", 3).
hasOscar("Parasite", 3).
hasOscar("Pulp Fiction", 3).
hasOscar("The Social Network", 3).
hasOscar("Schindler’s List", 3).
hasOscar("The Avengers", 1).
hasOscar("Avatar", 3).
hasOscar("The Lion King", 1).
hasOscar("Django Unchained", 3).
hasOscar("Interstellar", 1).
hasOscar("The Silence of the Lambs", 3).

question1(X1):-
    write("Is this movie made in or after 2000?"), nl,
    write("3. Yes"), nl,
    write("2. Probably yes"), nl,
    write("1. Probably no"), nl,
    write("0. No"), nl,
    read(X1).

question2(X2):-
    write("Is the original language of the film English?"), nl,
    write("3. Yes"), nl,
    write("2. Probably yes"), nl,
    write("1. Probably no"), nl,
    write("0. No"), nl,
    read(X2).

question3(X3):-
    write("Is the film based on real events?"), nl,
    write("3. Yes"), nl,
    write("2. Probably yes"), nl,
    write("1. Probably no"), nl,
    write("0. No"), nl,
    read(X3).

question4(X4):-
    write("Is the film's genre science fiction?"), nl,
    write("3. Yes"), nl,
    write("2. Probably yes"), nl,
    write("1. Probably no"), nl,
    write("0. No"), nl,
    read(X4).

question5(X5):-
    write("Are there any comedy elements in the film?"), nl,
    write("3. Yes"), nl,
    write("2. Probably yes"), nl,
    write("1. Probably no"), nl,
    write("0. No"), nl,
    read(X5).

question6(X6):-
    write("Did the film win at least one Oscar?"), nl,
    write("3. Yes"), nl,
    write("2. Probably yes"), nl,
    write("1. Probably no"), nl,
    write("0. No"), nl,
    read(X6).

pr:-	question1(X1),question2(X2),question3(X3),question4(X4),
		question5(X5),question6(X6),
		after2000(X,X1),inEnglish(X,X2),basedOnTrueStory(X,X3),sciFi(X,X4),
		hasComedy(X,X5),hasOscar(X,X6),
		write(X).