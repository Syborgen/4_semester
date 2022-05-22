#include <iostream>
#include <cstdlib>
using namespace std;

int main() {
	setlocale(LC_ALL, "Russian");
	int Z[6];
	int M[7] = {1,4, 1, 2, 1, 3, 1};
	int A[6] = {8,65,19,11,12,16};
	int N = 5;
	for (int k = 1; k < 6; k++) {
		if (A[N + 1 - k] == 0 || M[N + 2 - k] == 0) {
			cout << "M[" << k << "] = " << M[k] << endl << "A[" << N + 1 - k << "] = " << A[N + 1 - k] << endl << "M[" << N + 2 - k << "] = " << M[N + 2 - k] << endl;
			cout << "Деление на 0" << endl << endl;
		}
		else {
			Z[k] = k * M[k] - abs(A[k]) / A[N + 1 - k] / M[N + 2 - k];
			cout << "M[" << k << "] = " << M[k] << endl << "A[" << N + 1 - k << "] = " << A[N + 1 - k] << endl << "M[" << N + 2 - k << "] = " << M[N + 2 - k] << endl;
			cout << "Z[" << k << "] = " << k << " * " << M[k] << " - abs(" << A[k] << ") / " << A[N + 1 - k] << " / " << M[N + 2 - k] << " = " << Z[k] << endl << endl;
		}
	}
	   	  
	system("pause");
}