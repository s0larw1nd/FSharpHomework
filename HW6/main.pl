mother(X,Y) :- woman(X),parent(X,Y).
mother(X) :- mother(Y,X),print(Y).
brother(X,Y) :- man(X),X\=Y,mother(Z,X),mother(Z,Y).
brothers(X) :- brother(Y,X),print(Y),fail.
b_s(X,Y) :- mother(Z,X),mother(Z,Y).

father(X,Y) :- man(X),parent(X,Y).
father(X) :- father(Y,X),print(Y).
wife(X,Y) :- woman(X),parent(X,Z),parent(Y,Z).
wife(X) :- wife(Y,X),print(Y),!.

grand_ma(X,Y) :- woman(X),parent(Z,Y),parent(X,Z).
grand_mas(X) :- grand_ma(Y,X),print(Y),fail.

grand_pa_and_da(X,Y) :- man(X),woman(Y),parent(Z,Y),parent(X,Z),!;man(Y),woman(X),parent(Z,X),parent(Y,Z),!.
%grand_pa_and_da(X,Y) :- woman(Y),parent(Z,Y),father(X,Z),!;woman(X),parent(Z,X),father(Y,Z),!.
niece(X,Y) :- woman(X),parent(Z,X),Z\=Y,mother(G,Z),mother(G,Y),!.
%niece(X,Y) :- woman(X),parent(Z,X),Z\=Y,brother(Z,Y);woman(X),parent(Z,X),Z\=Y,sister(Z,Y)
niece(X) :- niece(Y,X),print(Y),fail.