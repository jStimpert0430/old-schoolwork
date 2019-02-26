//Joshua Stimpert
//Nov 25 2016
//A pizza class program
#include <iostream>;
#include <string>;
#include <locale>;//needed to send strings to lowercase
using namespace std;
string captureSize();
string captureCrust();
int captureToppings(string);

class Pizza{
public://public members
	Pizza();
	Pizza(string,string,int,int);
	string getCrust();
	string getSize();
	void setCrust(string);
	void setSize(string);
	int getPep();
	int getCheese();
	void setPep(int);
	void setCheese(int);
	void outputDescription();
	double computePrice();

private://private members
	string crust;
	string size;
	int topping1;
	int topping2;
};

Pizza::Pizza(){//default constructor
	setSize("Small");
	setCrust("Deep Dish");
	setPep(1);
	setCheese(1);
}

Pizza::Pizza(string newCrust, string newSize, int newTop1, int newTop2){//full arg constructor
	setSize(newSize);
	setCrust(newCrust);
	setPep(newTop1);
	setCheese(newTop2);
}

//get methods
string Pizza::getCrust(){
	return crust;
}

string Pizza::getSize(){
	return size;
}

int Pizza::getPep(){
	return topping1;
}

int Pizza::getCheese(){
	return topping2;
}

//set methods
void Pizza::setCrust(string newCrust){
	crust = newCrust;
}

void Pizza::setSize(string newSize){
	size = newSize;
}

void Pizza::setPep(int newTop){
	topping1 = newTop;
}

void Pizza::setCheese(int newTop){
	topping2 = newTop;
}

//calc pizza price method
double Pizza::computePrice(){
	double cost = 0;
	if(size == "small" || size == "s")
		cost = 10 + (2 * (topping1 + topping2));
	else if(size == "medium" || size == "m")
		cost = 14 + (2 * (topping1 + topping2));
	else
		cost = 17 + (2 * (topping1 + topping2));
	return cost;
}

//toString method
void Pizza::outputDescription(){
	cout<<"\nYou've ordered a "<<size<<" "<<crust<<" pizza with -"<<endl;
	cout<<"Pepperoni Toppings: "<<topping1<<endl;
	cout<<"Cheese Toppings: "<<topping2<<endl;
	cout<<"Total Price: $"<<computePrice()<<endl<<endl;
}
//end of class declarations

//main method
void main(){
	cout.setf(ios::fixed);
	cout.setf(ios::showpoint);
	cout.precision(2);
	string size = captureSize();
	string crust = captureCrust();
	int topPep = captureToppings("pepperoni");
	int topCheese = captureToppings("cheese");
	Pizza newPizza(crust, size, topPep, topCheese);
	newPizza.outputDescription();
}

//method to obtain a valid pizza size for use later
string captureSize(){
	string input;
	do{//loop to obtain valid input
		cout<<"What size of pizza would you like? (S)mall, (M)edium, or (L)arge: ";
		cin>>input;
		for(string::size_type i = 0; i < input.length(); i++)//Loop to convert string into all lower cased for easier comparison
			input[i] = tolower(input[i]);
		if(input == "small" || input == "medium" || input == "large" || input == "s" || input == "m" || input == "l"){
			if(input == "s")
				input = "small";
			else if(input == "m")
				input = "medium";
			else if(input == "l")
				input = "large";
			break;//break from loop once valid input is received
		}
		else
			cout<<"I don't know this size, please try again.\n";
	}while(true);//try again on fail
	return input;
}

//method to obtain valid crust type for use later
string captureCrust(){
	string input;
	do{//loop to obtain valid input
		cout<<"What crust would you like on your pizza? (H)andtossed, (D)eepdish, or (P)an: ";
		cin>>input;
		for(string::size_type i = 0; i < input.length(); i++)//Loop to convert string into all lower cased for easier comparison
			input[i] = tolower(input[i]);
		if(input == "handtossed" || input == "deepdish" || input == "pan" || input == "h" || input == "d" || input == "p"){
			if(input == "h")
				input = "handtossed";
			else if(input == "d")
				input = "deepdish";
			else if(input == "p")
				input = "pan";
			break;//break from loop once valid input is recieved
		}
		else
			cout<<"I don't know this crust, please try again.\n";
	}while(true);//try again on fail
	return input;
}

//generic method to capture number of toppings
int captureToppings(string toppingType){
	int input;
	do{//loop to obtain valid input
		cout<<"How many "<<toppingType<< " toppings you would like? ";
		cin>>input;
		if(input >= 0)
			break;//break from loop once valid input is recieved
		else
			cout<<"Toppings must be a positive number. Please try again.\n";
	}while(true);//try again on fail
	return input;
}
