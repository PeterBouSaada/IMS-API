class RenameTypeToCategoryInParts < ActiveRecord::Migration[7.0]
  def up
    rename_column :parts, :type, :category
  end

  def down
    rename_column :parts, :category, :type
  end
end
