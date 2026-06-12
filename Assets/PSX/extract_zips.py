import os
import zipfile

def extract_all_zips():
    # Get the directory where the script is located
    current_dir = os.path.dirname(os.path.abspath(__file__))
    
    # List all files in the current directory
    files = os.listdir(current_dir)
    
    # Filter for ZIP files
    zip_files = [f for f in files if f.lower().endswith('.zip')]
    
    if not zip_files:
        print("No ZIP files found in the current directory.")
        return

    print(f"Found {len(zip_files)} ZIP file(s). Starting extraction...")

    for zip_name in zip_files:
        zip_path = os.path.join(current_dir, zip_name)
        # Create a folder name based on the zip file name (without .zip extension)
        extract_to = os.path.join(current_dir, os.path.splitext(zip_name)[0])
        
        # Create the directory if it doesn't exist
        if not os.path.exists(extract_to):
            os.makedirs(extract_to)
            
        print(f"Extracting '{zip_name}' to '{extract_to}'...")
        
        try:
            with zipfile.ZipFile(zip_path, 'r') as zip_ref:
                zip_ref.extractall(extract_to)
            print(f"Successfully extracted '{zip_name}'.")
        except zipfile.BadZipFile:
            print(f"Error: '{zip_name}' is a bad zip file.")
        except Exception as e:
            print(f"An error occurred while extracting '{zip_name}': {e}")

if __name__ == "__main__":
    extract_all_zips()
