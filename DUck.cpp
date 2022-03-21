#include <string>
#include <iostream>
using namespace std;

class Swim{
public: virtual void Iswim() = 0;
};
class Swimming :public Swim{
public: void Iswim() override { cout << "just swim....nothing"<<endl;}
};
class Littleswimming :public Swim{
public: void Iswim() override { cout << "Swimming's sounds"<<endl;}
};

class Quack{
public: virtual void quack()=0;
};
class NormalQuack : public Quack {
public:void quack() override{
cout << "QUACK"<<endl;}
};
class LittleQuack : public Quack{
public:void quack() override {
cout << "quack"<<endl;
}
};

class fly{
public: virtual void Fly() = 0;};
class Can_fly :public fly {
public:void Fly() override { cout << "I see sky"<<endl;}
};
class Never_fly :public fly {
public:void Fly() override { cout << "I'm never can fly!"<<endl; }
};


class Displey
{
private: string name;
public: Displey(string name) {
  this->name = name;}
   void Print(){
     cout << name<<"HELLO!"<<endl;}
};

class Duck
{
private:
    Quack &quackStrategy;
    Swim &swimStrategy;
    fly &flyStrategy;
    Displey &displey;
public:
Duck(Displey &disp, Quack &quack,Swim &swim, fly &fly) : displey(disp), quackStrategy(quack), swimStrategy(swim),flyStrategy(fly){}
    void Print() {displey.Print();}
    void Quack() { quackStrategy.quack(); }
    void Swim() { swimStrategy.Iswim(); }
    void Fly() { flyStrategy.Fly(); }
};
 
void qra(Duck& duck)
{
    duck.Print();
    duck.Swim();  
    duck.Quack();
    duck.Fly();
}
 
  int main(){
    Duck little_duck = *new Duck(*new Displey("Маленькая утка"), *new     LittleQuack(),*new Swimming(),*new Never_fly());
    Duck Rubber_Duck = *new Duck(*new Displey("Резиновая утка"),*new LittleQuack(), *new Littleswimming(), *new Can_fly());
    qra(little_duck);
    qra(Rubber_Duck);
    return 0;}
 