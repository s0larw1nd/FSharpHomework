mother(X,Y) :- woman(X),parent(X,Y).
mother(X) :- mother(Y,X),print(Y).
brother(X,Y) :- man(X),X\=Y,mother(Z,X),mother(Z,Y).
brothers(X) :- brother(Y,X),print(Y),fail.
b_s(X,Y) :- mother(Z,X),mother(Z,Y).

father(X,Y) :- man(X),parent(X,Y).
father(X) :- father(Y,X),print(Y).
wife(X,Y) :- woman(X),parent(X,Z),parent(Y,Z).
wife(X) :- wife(Y,X),print(Y),!.