# scatter plot
import pandas as pd
import matplotlib.pyplot as plt
from scipy.stats import pearsonr

# Load the data from the CSV file
data = pd.read_csv('pupil_diameter_data.csv')

# Filter out negative values
data = data[data['Left Pupil Diameter'] >= 0]
data = data[data['Right Pupil Diameter'] >= 0]

# Calculate the correlation coefficient (R-value)
correlation, _ = pearsonr(data['Right Pupil Diameter'], data['Left Pupil Diameter'])

# Create the scatter plot
plt.figure(figsize=(8, 6))
plt.scatter(data['Right Pupil Diameter'], data['Left Pupil Diameter'], marker='o', s=40, alpha=0.7)
plt.xlabel('Right Pupil Diameter')
plt.ylabel('Left Pupil Diameter')
plt.title(f'Correlation between Left and Right Pupil Diameter (R = {correlation:.2f})')
plt.grid(True)
plt.show()
