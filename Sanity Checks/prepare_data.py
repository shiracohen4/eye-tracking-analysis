import csv

data_file = 'data.csv'
data2_file = 'data2.csv'
eye_openness_file = 'eye_openness_data.csv'
pupil_diameter_file = 'pupil_diameter_data.csv'
pupil_position_y_file = 'pupil_position_y_data.csv'


# Change header of the file

def is_number(string):
    try:
        float(string)
        return True
    except ValueError:
        return False


# Extract eye openness data
with open(data_file, 'r') as f_in, open(eye_openness_file, 'w', newline='') as f_out:
    reader = csv.reader(f_in)
    writer = csv.writer(f_out)

    # Write the header line
    header = next(reader)[:3]  # Select the first three columns
    writer.writerow(header)

    for row in reader:
        if len(row) > 1 and is_number(
                row[0]) and '----------------------------------NEW SCENE----------------------------------' not in row:
            # Select the first three columns
            new_row = row[:3]

            # Write the selected columns to the output file
            writer.writerow(new_row)


# Extract pupil diameter data
with open(data_file, 'r') as f_in, open(pupil_diameter_file, 'w', newline='') as f_out:
    reader = csv.reader(f_in)
    writer = csv.writer(f_out)

    # Write the header line
    header = ['Frame', 'Left Pupil Diameter', 'Right Pupil Diameter']
    writer.writerow(header)

    for row in reader:
        if len(row) > 1 and is_number(row[0]) and '----------------------------------NEW SCENE----------------------------------' not in row:
            # Extract the frame column and the last two columns
            new_row = [row[0], row[-2], row[-1]]

            # Write the selected columns to the output file
            writer.writerow(new_row)


# Extract y values of pupil position data
def clean_pupil_position(position):
    # Remove the closing parenthesis from the string
    return position.rstrip(')')


with open(data2_file, 'r') as f_in, open(pupil_position_y_file, 'w', newline='') as f_out:
    reader = csv.reader(f_in)
    writer = csv.writer(f_out)

    # Write the header line
    header = ['Frame', 'Left Pupil Position y', 'Right Pupil Position y']
    writer.writerow(header)

    for row in reader:
        if len(row) > 1 and is_number(row[0]) and '----------------------------------NEW SCENE----------------------------------' not in row:
            # Extract the Frame, Left Pupil Position y, and Right Pupil Position y columns
            frame = row[0]
            left_pupil_y = clean_pupil_position(row[4])
            right_pupil_y = clean_pupil_position(row[6])

            # Write the selected columns to the output file
            writer.writerow([frame, left_pupil_y, right_pupil_y])