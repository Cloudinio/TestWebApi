SELECT
  e."Id"            AS exercise_id,
  e."Name"          AS exercise_name,
  e."CreatedAt"     AS exercise_created,
  t."Id"            AS type_id,
  t."Name"          AS type_name,
  t."CreatedAt"     AS type_created
FROM
  "Exercises"       AS e
  JOIN "ExerciseTypes" AS t
    ON e."ExerciseTypeId" = t."Id"
ORDER BY
  e."Id";