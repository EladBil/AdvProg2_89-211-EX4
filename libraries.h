#pragma once
#include "timeseries.h"
#include "HybridAnomalyDetector.h"
/*
* TimeSeries
*/
extern "C" __declspec(dllexport) void* createTS(const char* CSVfileName) {
	return (void*) new TimeSeries(CSVfileName);
}

extern "C" __declspec(dllexport) vector<float> TsGetAttributeData(TimeSeries * ts, string name) {
	return ts->getAttributeData(name);
}
extern "C" __declspec(dllexport) vector<string> TsGetAttributes(TimeSeries * ts) {
	return ts->gettAttributes();
}
extern "C" __declspec(dllexport) size_t TsGetRowSize(TimeSeries * ts) {
	return ts->getRowSize();
}

/*
* SimpleAnomalyDetector
*/
extern "C" __declspec(dllexport) void* createSad() {
	return (void*) new SimpleAnomalyDetector();
}
extern "C" __declspec(dllexport) void SadLearnNormal(SimpleAnomalyDetector * s, const TimeSeries & ts) {
	s->learnNormal(ts);
}
extern "C" __declspec(dllexport)  vector<AnomalyReport> SadDetect(SimpleAnomalyDetector * s, const TimeSeries & ts) {
	return s->detect(ts);
}
extern "C" __declspec(dllexport) vector<correlatedFeatures> SadGetNormalModel(SimpleAnomalyDetector * s) {
	return s->getNormalModel();
}
extern "C" __declspec(dllexport) void SadSetCorrelationThreshold(SimpleAnomalyDetector* s, float threshold) {
	s->setCorrelationThreshold(threshold);
}

/*
* HybridAnomalyDetector (Actually only finds circles)
*/
extern "C" __declspec(dllexport) void* createHad() {
	return (void*) new HybridAnomalyDetector();
}
extern "C" __declspec(dllexport) void HadLearnNormal(HybridAnomalyDetector * h, const TimeSeries & ts) {
	h->learnNormal(ts);
}
extern "C" __declspec(dllexport) vector<AnomalyReport> HadDetect(SimpleAnomalyDetector * h, const TimeSeries & ts) {
	return h->detect(ts);
}


