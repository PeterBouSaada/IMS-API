# This file should contain all the record creation needed to seed the database with its default values.
# The data can then be loaded with the bin/rails db:seed command (or created alongside the database with db:setup).
#
# Examples:
#
#   movies = Movie.create([{ name: "Star Wars" }, { name: "Lord of the Rings" }])
#   Character.create(name: "Luke", movie: movies.first)

Part.destroy_all
Machine.destroy_all

100.times do |index|
  machine = Machine.create!(name: "Name #{index}",
                  location: "Location #{index}",
                  picture: "Picture #{index}")
  Part.create!(number: "Number #{index}",
               category: "Category #{index}",
               machine_id: machine.id)
end

p "Created #{Machine.count} machines and #{Part.count} parts"