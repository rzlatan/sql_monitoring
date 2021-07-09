import sys
import csv
import numpy as np
from sklearn.linear_model import LinearRegression
import pandas as pd
from datetime import date, datetime, timedelta

report = sys.argv[1]

train_set_file_path = "C:\SQLProjects\sql_monitoring\SQLMonitoring\SQLMonitoring\SQLMonitoring\\" + report + "\smart_scaling_cpu.csv"
columns = ["Day", "Hour", "Minute", "CPU"]
sheet = pd.read_csv(train_set_file_path, usecols=columns)
days = sheet["Day"].values
hours = sheet["Hour"].values
minutes = sheet["Minute"].values
cpu = sheet["CPU"].values

i = 0
x = []
y = []

while i < len(days):
    element = [days[i], hours[i], minutes[i]]
    x.append(element)
    y.append(cpu[i])
    i = i + 1

model = LinearRegression(normalize=True)
model.fit(x, y)

dt = datetime.now()
last_x = [ days[len(days) - 1], hours[len(hours)-1], minutes[len(minutes)-1]]
day = last_x[0]
hour = last_x[1]
minute = last_x[2]
i = 0
predictions = []
while i < 1000:
    dt = dt + timedelta(minutes=10)
    minute = minute + 10
    if minute >= 60:
        minute = minute % 10
        hour = hour + 1
        if hour >= 24:
            hour = 0
            day = day + 1
            if day == 7:
                day = 0
    prediction = model.predict([[day, hour, minute]])
    predictions.append([dt.strftime("%m/%d/%Y, %H:%M:%S"), prediction[0]])
    i = i + 1

result = np.array(predictions)
result_file_path = "C:\SQLProjects\sql_monitoring\SQLMonitoring\SQLMonitoring\SQLMonitoring\\" + report + "\cpu_result.csv"
pd.DataFrame(result).to_csv(result_file_path)