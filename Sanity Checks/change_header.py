import csv

# Input file name
input_file = "check.csv"

# Define the new header line
new_header = [
    "Frame",
    "Left Eye openness",
    "Right Eye Openness",
    "Left Pupil Position X",
    "Left Pupil Position Y",
    "Right Pupil Position X",
    "Right Pupil Position Y",
    "Left Pupil Diameter",
    "Right Pupil Diameter"
]

# Write the new header to "data2.csv" and replace the previous content
with open("data2.csv", 'w', newline='') as output_csv:
    writer = csv.writer(output_csv)

    # Write the new header to the output file
    writer.writerow(new_header)

# Read data from the input file and append to "data2.csv" without the header
with open(input_file, 'r', newline='') as input_csv, open("data2.csv", 'a', newline='') as output_csv:
    reader = csv.reader(input_csv)
    writer = csv.writer(output_csv)

    # Skip the header in the input file
    next(reader, None)

    # Copy the remaining rows from the input to the output
    for row in reader:
        writer.writerow(row)

print(f"Header in data2.csv has been replaced with the new header.")
