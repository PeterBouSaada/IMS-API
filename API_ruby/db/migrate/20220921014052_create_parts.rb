class CreateParts < ActiveRecord::Migration[7.0]
  def change
    create_table :parts do |t|
      t.string :number
      t.string :type
      t.belongs_to :machine, null: false, foreign_key: true

      t.timestamps
    end
  end
end
