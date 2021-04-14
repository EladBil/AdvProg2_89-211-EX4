/*
* NOTE:
* Everytime you receive a non primitive type, you have to recieve it as an IntPtr in c#!!!!
* The only way to use that IntPtr once you have it in c# is to call one (or some) of the functions in this header.
* That is the ONLY way to access the data inside the IntPtr.
* Once you finished with the intptr, call the appropriate delete function from this library so we don't
* get a memory leak!
*/
#pragma once
#include "timeseries.h"
#include "HybridAnomalyDetector.h"
#define EXPORT extern "C" __declspec(dllexport)

//test
EXPORT int testing() {
	std::cout << "DLL WORKS" << endl;
	return 1;
}

/*
* 
* 
* TimeSeries
* 
* 
*/
EXPORT void* createTS(const char* CSVfileName) {
	return (void*) new TimeSeries(CSVfileName);
}


//gives amount of attributes (amount of items per row)
EXPORT size_t TsGetAttributesSize(TimeSeries * ts) {
	return ts->getAttributesSize();
}
//Gives length of colomns (how many lines are in the timeseries)
EXPORT size_t TsGetColSize(TimeSeries * ts) {
	return ts->getRowSize();
}

/*
Recive an IntPtr on c# side. To get item from index of row, call TsGetInRow.
so it should be:
		[DllImport("AP2Libraries.dll")]
		public static extern IntPtr TsGetRow(IntPtr ts, int j);
MAKE SURE TO DELETE THE ROW WHEN DONE by calling TsDeleteRow!!
*/
EXPORT vector<float>* TsGetRow(TimeSeries * ts, int j) {
	vector<float>* v = new vector<float>();
	*v = ts->getRow(j);
	return v;
}
/*
* same for this. Use TsGetInRow and TsDeleteRow when done
* returns the colomn at given index
*/
EXPORT vector<float>* TsGetColByIndex(TimeSeries* ts, int i) {
	vector<float>* v = new vector<float>();
	*v = ts->getAttributeData(ts->gettAttributes().at(i));
	return v;
}
//returns item i in the row you recieved from TsGetRow
EXPORT float TsGetInRow(vector<float>* row, int i) {
	return row->at(i);
}
EXPORT void TsDeleteRow(vector<float>* row) {
	delete row;
}
//delete TS
EXPORT void TsDelete(TimeSeries* ts) {
	delete ts;
}


/*
*
* 
* SimpleAnomalyDetector
* 
*/
/*
EXPORT void* createAd() {
	return (void*) new SimpleAnomalyDetector();
}
EXPORT void AdLearnNormal(SimpleAnomalyDetector * s, const TimeSeries & ts) {
	s->learnNormal(ts);
}
EXPORT void AdDelete(SimpleAnomalyDetector* s) {
	delete s;
}
//recieve as an IntPtr
EXPORT  vector<AnomalyReport>* AdDetect(SimpleAnomalyDetector * s, const TimeSeries & ts) {
	vector<AnomalyReport>* v = new vector<AnomalyReport>();
	*v = s->detect(ts);
	return v;
}
*/

//gets size of given anomaly report, ALSO WORKS FOR HYBRID!!!!
EXPORT int reportSize(vector<AnomalyReport>* v) {
	return v->size();
}
//gets tempstep of given anomaly at index i, ALSO WORKS FOR HYBRID!!!!
EXPORT int reportGetAtIndex(vector<AnomalyReport>* v, int i) {
	return v->at(i).timeStep;
}
//deletes anomaly report, ALSO WORKS FOR HYBRID!!!!
EXPORT void reportDelete(vector<AnomalyReport>* v) {
	delete v;
}

/*
* 
* 
* HybridAnomalyDetector (Actually only finds circles)
* 
* 
*/

EXPORT void* createAd() {
	return (void*) new HybridAnomalyDetector();
}
EXPORT void AdLearnNormal(HybridAnomalyDetector * h, const TimeSeries & ts) {
	h->learnNormal(ts);
}
//gives anomaly report. Make sure to delete when done and to access the report detaild, use report functions above
EXPORT  vector<AnomalyReport>* AdDetect(HybridAnomalyDetector* h, const TimeSeries& ts) {
	vector<AnomalyReport>* v = new vector<AnomalyReport>();
	*v = h->detect(ts);
	return v;
}

EXPORT void AdDelete(HybridAnomalyDetector * had) {
	delete had;

}


/*
* 
* Pearson
* gets pearson from colomn index
* 
*/
EXPORT float pearsonFromColIndex(TimeSeries * ts, int col1, int col2) {
	vector<float> * v1 = TsGetColByIndex(ts, col1);
	vector<float>* v2 = TsGetColByIndex(ts, col2);
	float* x = v1->data();
	float* y = v2->data();
	float ans =  pearson(x, y, v1->size());
	delete v1;
	delete v2;
	return ans;
	
}

/*
* 
* 
* Cov, var, avg from Ts colomn to calculate linear reg
* 
* 
*/
EXPORT float covFromColIndex(TimeSeries* ts, int col1, int col2) {
	vector<float>* v1 = TsGetColByIndex(ts, col1);
	vector<float>* v2 = TsGetColByIndex(ts, col2);
	float* x = v1->data();
	float* y = v2->data();
	float ans = cov(x, y, v1->size());
	delete v1;
	delete v2;
	return ans;
}
EXPORT float varFromColIndex(TimeSeries* ts, int col) {
	vector<float>* v1 = TsGetColByIndex(ts, col);
	float* x = v1->data();
	float ans = var(x, v1->size());
	delete v1;
	return ans;
}
EXPORT float avgFromColIndex(TimeSeries* ts, int col) {
	vector<float>* v1 = TsGetColByIndex(ts, col);
	float* x = v1->data();
	float ans = avg(x, v1->size());
	delete v1;
	return ans;
}


/*
*
* Min Circle
* 
*/

//gives you min circle from x's and y's
EXPORT Circle* CircleGetMinCircle(float* x, float* y, int size) {
	Point** p = new Point * [size];
	for (int i = 0; i < size; i++) {
		p[i] = new Point(x[i], y[i]);
	}
	Circle circ = findMinCircle(p, size);
	for (int i = 0; i < size; i++) {
		delete p[i];
	}
	delete[] p;
	Circle * c = new Circle(circ.center, circ.radius);
	return c;
	
}

//gives you radius of min circle
EXPORT float CircleGetRadius(Circle* circ) {
	return circ->radius;
}

//x of min circle
EXPORT float CircleGetCenterX(Circle* circ) {
	return circ->center.x;
}

//y of min circle
EXPORT float CircleGetCenterY(Circle* circ) {
	return circ->center.y;
}

//Delete min circle
EXPORT void CircleDelete(Circle* circ) {
	delete circ;
}